using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Enums;
using Microsoft.AspNetCore.Identity;

namespace GolfingAppUI.Helpers;
/// <summary>
/// This static class handles database seeding of initial values
/// </summary>

public static class Seeders{

    //Seeds Admin and Roles
    public static async Task SeedRolesAndDefaultUser(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            //Create User Roles
            string[] roles = { Roles.Admin.ToString(), Roles.Customer.ToString() };
            foreach (var role in roles)
            {
                var exists = await roleManager.RoleExistsAsync(role);
                if(!exists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Administrator profile
            var email = configuration.GetSection("Administrator:Email").Value;
            var password = configuration.GetSection("Administrator:Password").Value;
            var user = new ApplicationUser{
                UserName = "Administrator",
                Email = email,
                EmailConfirmed = true
                };

            if (await userManager.FindByEmailAsync(email) is null)
            {
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                
            }
        }
      
        
}