using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using eMedicNETv7.Data;
using eMedicNETv7.Services;
using eMedicNETv7.Extensions;
using eMedicEntityModel.Models.v1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.Configure<CookiePolicyOptions>(options => {
	options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.Configure<ForwardedHeadersOptions>(options => {
	options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("Default"), new MySqlServerVersion(new Version(8, 0, 11))));

var _appIdentitySettings = builder.Configuration.GetSection("AppIdentitySettings");
var appIdentitySettings = _appIdentitySettings.Get<AppIdentitySettings>();

//Inject AppIdentitySettings so that can be used anywhere.
builder.Services.Configure<AppIdentitySettings>(_appIdentitySettings);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
	options.Stores.MaxLengthForKeys = 128;

	// User Settings
	options.User.RequireUniqueEmail = appIdentitySettings.User.RequireUniqueEmail;

	// Password settings
	options.Password.RequireDigit = appIdentitySettings.Password.RequireDigit;
	options.Password.RequiredLength = appIdentitySettings.Password.RequiredLength;
	options.Password.RequireLowercase = appIdentitySettings.Password.RequireLowerCase;
	options.Password.RequireUppercase = appIdentitySettings.Password.RequireUpperCase;
	options.Password.RequireNonAlphanumeric = appIdentitySettings.Password.RequireNonAlphaNumeric;

	//Lockout Settings
	options.Lockout.AllowedForNewUsers = appIdentitySettings.Lockout.AllowedForNewUsers;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(appIdentitySettings.Lockout.DefaultLockoutTimeSpanInMins);
	options.Lockout.MaxFailedAccessAttempts = appIdentitySettings.Lockout.MaxFailedAccessAttempts;

	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ILoadData, LoadData>();
//builder.Services.AddTransient<ISMSService, SMSService>();
//builder.Services.AddTransient<IAddEventLog, AddEventLog>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(o => {
		o.LoginPath = "~/Account/Login";
		o.LogoutPath = "~/Account/Logout";
	})
	.AddJwtBearer(o => {
		o.TokenValidationParameters = new TokenValidationParameters
		{
			ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
			ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
		};
	});
builder.Services.AddAntiforgery(options => {
	options.FormFieldName = "AntiforgeryFieldName";
	options.HeaderName = "X-CRSF-TOKEN-HEADERNAME";
	options.SuppressXFrameOptionsHeader = false;
});

builder.Services.AddMvc();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
	builder.WebHost.ConfigureKestrel(options => {
		options.AllowSynchronousIO = true;
	});

	builder.WebHost.UseKestrel(options => {
		options.ListenAnyIP(2111);
		options.Limits.MaxConcurrentConnections = 100;
		options.Limits.MaxConcurrentUpgradedConnections = 100;
		options.Limits.MaxRequestBodySize = 52428800;
	});
}


//AWS
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
builder.WebHost.UseIISIntegration();

#region Swagger Configuration
builder.Services.AddSwaggerGen(c => {
	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Title = "eMedicNET Web API",
		Version = "v1",
		Description = "An API to perform eMedicNET Web API functionality",
		TermsOfService = new Uri("https://app.stgcleaningservices.com/terms"),
		Contact = new Microsoft.OpenApi.Models.OpenApiContact
		{
			Name = "VCS Team",
			Email = "support@vcs-asia.com",
		}
	});
	//c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer() { Url = $"https://app.stgcleaningservices.com" });
	//c.AddSecurityDefinition("XApiKey", new OpenApiSecurityScheme
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		/*
        In = ParameterLocation.Header,
        Description = "ApiKey must appear in header",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "ApiKey",
        Scheme = "XApiKey",
        */
		In = ParameterLocation.Header,
		Description = "Please insert the token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer",
	});
	c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
                    /*
                    Type = ReferenceType.SecurityScheme,
                    Id = "XApiKey",*/
                    Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{ }
		}
	});
});
#endregion
#region Authentication
//builder.Services.AddAuthentication("XApiKey").AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("XApiKeyAuthentication", null);
builder.Services.AddAuthorization().AddAuthentication().AddIdentityServerJwt();
#endregion
var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
//app.MapGet("/hello", () => "Hello World!").ExcludeFromDescription();
if (app.Environment.IsDevelopment())
{
	IdentityModelEventSource.ShowPII = true;
}
app.UseSwagger();
app.UseSwaggerUI(c => {
	c.SwaggerEndpoint("v1/swagger.json", "eMedicNET Web API v1");
});


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
//app.UseIdentityServer();
app.UseAuthorization();

Initiate.CreateUsersAndRoles(app);

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();