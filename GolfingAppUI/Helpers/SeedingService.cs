using GolfingDataAccessLib.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace GolfingAppUI.Helpers
{
    public class SeedingService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedingService(UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            //Seed database
            Seeders.SeedRolesAndDefaultUser(_roleManager, _userManager, _config).ConfigureAwait(false);

        }
    }
}
