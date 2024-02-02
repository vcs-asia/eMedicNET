using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

using eMedicAPIv2.Data;
using eMedicAPIv2.Extensions;
using eMedicEntityModel.Models.v1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvcCore();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "eMedic API",
        Version = "v2",
        Description = "An API to perform eMedic functionality",
        TermsOfService = new Uri("https://www.vcs-emedic.net"),
        Contact = new OpenApiContact
        {
            Name = "Support/Development",
            Email = "support@valuecreatingsolutions.com",
        },
    });
    //c.AddServer(new OpenApiServer() {Url = $"https://www.vcs-emedic.com" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert the token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{ }
        },
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.Configure<ForwardedHeadersOptions>(options => {
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
});

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 1111)));
});

var aps = builder.Configuration.GetSection("AppIdentitySettings");
var appIdentitySettings = aps.Get<AppIdentitySettings>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
    options.Stores.MaxLengthForKeys = 128;

    options.User.RequireUniqueEmail = appIdentitySettings.User.RequireUniqueEmail;

    options.Password.RequireDigit = appIdentitySettings.Password.RequireDigit;
    options.Password.RequiredLength = appIdentitySettings.Password.RequiredLength;
    options.Password.RequireLowercase = appIdentitySettings.Password.RequireLowerCase;
    options.Password.RequireUppercase = appIdentitySettings.Password.RequireUpperCase;
    options.Password.RequireNonAlphanumeric = appIdentitySettings.Password.RequireNonAlphaNumeric;

    options.Lockout.AllowedForNewUsers = appIdentitySettings.Lockout.AllowedForNewUsers;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(appIdentitySettings.Lockout.DefaultLockoutTimeSpanInMins);
    options.Lockout.MaxFailedAccessAttempts = appIdentitySettings.Lockout.MaxFailedAccessAttempts;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    builder.WebHost.ConfigureKestrel(options => {
        options.AllowSynchronousIO = true;
    });

    builder.WebHost.UseKestrel(options => {
        options.ListenAnyIP(2006);
        options.Limits.MaxConcurrentConnections = 100;
        options.Limits.MaxConcurrentUpgradedConnections = 100;
        options.Limits.MaxRequestBodySize = 52428800;
    });
}


//AWS
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
builder.WebHost.UseIISIntegration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("v2/swagger.json", "eMedic API v2"));
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("v2/swagger.json", "eMedic API v2"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

new Initiate().CreateUsersAndRoles(app);

app.MapControllers();

app.Run();