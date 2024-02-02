using Microsoft.AspNetCore.Identity;
using eMedicNETv6.Data;
using eMedicEntityModel.Models.v1;

namespace eMedicNETv6.Extensions
{
    public static class Initiate
    {
        public static async void CreateUsersAndRoles(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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
                    string uName = "SuperAdmin";
                    string password = "Nimda123";

                    var success = await _userManager.CreateAsync(new ApplicationUser { UserName = uName, Email = user, EmailConfirmed = true }, password);
                    if (success.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(uName), "Super Administrator");
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("Administrator");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Administrator"));
                    // Check if the user exists
                    string user = "info@valuecreatingsolutions.com";
                    string uName = "Administrator";
                    string password = "Passw123";

                    var success = await _userManager.CreateAsync(new ApplicationUser { UserName = uName, Email = user, EmailConfirmed = true }, password);
                    if (success.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(uName), "Administrator");
                    }
                }

                roleCheck = await _roleManager.RoleExistsAsync("Manager");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Manager"));
                }

                roleCheck = await _roleManager.RoleExistsAsync("Clerk");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Clerk"));
                }

                roleCheck = await _roleManager.RoleExistsAsync("Sales");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Sales"));
                }

                roleCheck = await _roleManager.RoleExistsAsync("Purchases");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("Purchases"));
                }

                roleCheck = await _roleManager.RoleExistsAsync("GL");
                if (!roleCheck)
                {
                    identityResult = await _roleManager.CreateAsync(new ApplicationRole("GL"));
                }
            };
        }
    }
}
