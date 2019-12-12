using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly string UserNF = "User not found in the database!";

        public UserService(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            User user = await userRepository.ReadAsync(id);
            UserModel userModel = new UserModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userModel;
        }

        public async Task<List<UserInfoModel>> GetAllAsync()
        {
            List<User> users = _userManager.Users.ToList();
            List<UserInfoModel> userInfoModels = new List<UserInfoModel>();

            foreach (var item in users)
            {
                UserInfoModel user = new UserInfoModel();
                user.Id = item.Id;
                user.Email = item.Email;
                user.FirstName = item.FirstName;
                user.LastName = item.LastName;

                IList<string> role = await _userManager.GetRolesAsync(item);
                user.Role = role.FirstOrDefault();

                userInfoModels.Add(user);
            }

            return userInfoModels;
        }

        public List<RolesModel> GetAllRolesAsync()
        {
            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();
            List<RolesModel> roles = new List<RolesModel>();

            foreach (var item in identityRoles)
            {
                RolesModel role = new RolesModel();
                role.Role = item.NormalizedName;
                roles.Add(role);
            }

            return roles;
        }

        public async Task<BaseResponseModel> CreateAsync(UserModel userModel)
        {
            BaseResponseModel report = new BaseResponseModel();

            User user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email,
            };
            await userRepository.CreateAsync(user);

            return report;
        }

        public BaseResponseModel Update(UserModel userModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            User user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email,
            };
            userRepository.UpdateAsync(user);

            return report;
        }
        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            User user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                report.Message.Add(UserNF);
                report.IsValid = false;
                return report;
            }
            await userRepository.Delete(user);

            return report;
        }

        public async Task<RolesModel> ChangeRoleAsync(RolesModel rolesModel)
        {
            User user = _userManager.Users.Where(w => w.Id == rolesModel.Id.ToString()).FirstOrDefault();
            user.EmailConfirmed = true;
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, rolesModel.Role);

            return rolesModel;
        }
    }
}
