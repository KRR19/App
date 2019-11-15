using App.BussinesLogicLayer.Models;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.BussinesLogicLayer.Services
{
    public class IdentityRoleInitializer
    {
        public static IdentityResult SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            bool isUserCreate = roleManager.RoleExistsAsync(DefaultRoles.User).Result;
            bool isAdminCreate = roleManager.RoleExistsAsync(DefaultRoles.Admin).Result;
            IdentityResult result = new IdentityResult();

            if (!isUserCreate)
            {
                IdentityRole role = new IdentityRole();
                role.Name = DefaultRoles.User;
                role.NormalizedName = DefaultRoles.User;
                result = roleManager.CreateAsync(role).Result;
            }

            if (!isAdminCreate)
            {
                IdentityRole role = new IdentityRole();
                User user = new User();

                role.Name = DefaultRoles.Admin;
                role.NormalizedName = DefaultRoles.Admin;
                result = roleManager.CreateAsync(role).Result;

                user.UserName = "Anuitex@mail.com";
                user.Email = "Anuitex@mail.com";

                result = userManager.CreateAsync(user, "123456").Result;
                result = userManager.AddToRoleAsync(user, DefaultRoles.Admin.ToString()).Result;
            }

            return result;
        }
    }
}

