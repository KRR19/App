using App.BussinesLogicLayer.Models;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.BussinesLogicLayer.Services
{
    public class IdentityRoleInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private const string SuperAdminEmail = "Anuitex@mail.com";
        private const string SuperAdminPassword = "123456";

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
            bool isSuperAdminCreated = _userManager.FindByEmailAsync(SuperAdminEmail).Result != null;

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
                user.UserName = SuperAdminEmail;
                user.Email = SuperAdminEmail;
                user.EmailConfirmed = true;

                result = _userManager.CreateAsync(user, SuperAdminPassword).Result;
                result = _userManager.AddToRoleAsync(user, DefaultRoles.Admin).Result;
            }

            return result;
        }
    }
}

