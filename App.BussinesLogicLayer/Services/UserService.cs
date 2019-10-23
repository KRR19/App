using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<BaseResponseModel> Create(UserModel userModel)
        {
            BaseResponseModel report = new BaseResponseModel();

            User user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email,
                PasswordHash = userModel.PasswordHash
            };

            await userRepository.Create(user);

            return report;
        }
        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            User user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                report.Message.Add("User not found in the database!");
                return report;
            }

            await userRepository.Delete(user);
            return report;
        }
        public async Task<UserModel> GetById(Guid id)
        {
            User user = await userRepository.Read(id);
            UserModel userModel = new UserModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash
            };

            return userModel;
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
                PasswordHash = userModel.PasswordHash
            };
            userRepository.Update(user);
            return report;
        }
    }
}
