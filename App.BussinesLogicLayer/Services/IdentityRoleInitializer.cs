using App.BussinesLogicLayer.Models;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.BussinesLogicLayer.Services
{
    public class IdentityRoleInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityRoleInitializer(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IdentityResult SeedRoles()
        {
            IdentityResult result = new IdentityResult();
            bool isUserCreated = _roleManager.RoleExistsAsync(DefaultRoles.User).Result;
            bool isAdminCreated = _roleManager.RoleExistsAsync(DefaultRoles.Admin).Result;
            bool isSuperAdminCreated = _userManager.FindByEmailAsync("Anuitex@mail.com").Result != null;

            if (!isUserCreated)
            {
                IdentityRole role = new IdentityRole();
                role.Name = DefaultRoles.User;
                role.NormalizedName = DefaultRoles.User;
                result = _roleManager.CreateAsync(role).Result;
            }

            if (!isAdminCreated)
            {
                IdentityRole role = new IdentityRole();

                role.Name = DefaultRoles.Admin;
                role.NormalizedName = DefaultRoles.Admin;
                result = _roleManager.CreateAsync(role).Result;
            }

            if (!isSuperAdminCreated)
            {
                User user = new User();
                user.UserName = "Anuitex@mail.com";
                user.Email = "Anuitex@mail.com";
                user.EmailConfirmed = true;

                result = _userManager.CreateAsync(user, "123456").Result;
                result = _userManager.AddToRoleAsync(user, DefaultRoles.Admin).Result;
            }

            return result;
        }
    }
}

