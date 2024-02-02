using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog.Extensions.Logging;

using eMedicv5.Data;
using eMedicv5.Models;
using eMedicNETEMv1.Models;
using eMedicv5.Extensions;
using eMedicv5.Services;

namespace eMedicv5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .Build()
                );

            services.AddOptions();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.Configure<ForwardedHeadersOptions>(options => {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MyConnection"), new MySqlServerVersion(new Version(8, 0, 11))));

            var _appIdentitySettings = Configuration.GetSection("AppIdentitySettings");
            var appIdentitySettings = _appIdentitySettings.Get<AppIdentitySettings>();

            //Inject AppIdentitySettings so that can be used anywhere.
            services.Configure<AppIdentitySettings>(_appIdentitySettings);

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Stores.MaxLengthForKeys = 128;

                // User Settings
                options.User.RequireUniqueEmail = appIdentitySettings.User.RequireUniqueEmail;
                options.SignIn.RequireConfirmedAccount = true;

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
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "~/Account/Login";
                    options.LogoutPath = "~/Account/Logout";
                });

            services.AddAntiforgery(options => {
                options.FormFieldName = "AntiforgeryFieldName";
                options.HeaderName = "X-CRSF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });
            services.AddControllersWithViews();
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myLog-{Date}.text");
            app.UseStatusCodePagesWithReExecute("/Home/HandleNotFound/{0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            CreateUsersAndRoles(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public async void CreateUsersAndRoles(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //if there is already an Administrator role, abort
                var _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                IdentityResult identityResult;
                var roleCheck = await _roleManager.RoleExistsAsync("Super Administrator");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Super Administrator"));
                    // Check if the user exists
                    string user = "admin@valuecreatingsolutions.com";
                    string password = "Bolla123";

                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                    var success = await _userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
                    if (success.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Super Administrator");
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("Administrator");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Administrator"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }
                roleCheck = await _roleManager.RoleExistsAsync("Doctor");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Doctor"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }
                roleCheck = await _roleManager.RoleExistsAsync("Purchase");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Purchase"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("Dispense");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Dispense"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("Cash");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Cash"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("Reception");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Reception"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("View");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("View"));
                    if (!identityResult.Succeeded)
                    {
                        Console.WriteLine(identityResult.Errors);
                    }
                }
            };
        }
    }
}
