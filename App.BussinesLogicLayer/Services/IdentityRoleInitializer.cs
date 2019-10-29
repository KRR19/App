using App.BussinesLogicLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace App.BussinesLogicLayer.Services
{
    class IdentityRoleInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(DefaultRoles.User).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = DefaultRoles.User;
                roleManager.CreateAsync(role);
            }

            if (!roleManager.RoleExistsAsync(DefaultRoles.Admin).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = DefaultRoles.Admin;
                roleManager.CreateAsync(role);
            }
        }
    }
}
