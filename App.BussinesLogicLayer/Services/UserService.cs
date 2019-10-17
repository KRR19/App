using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public  class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<BaseResponseModel> Create(UserModel userModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            IUserRepository userRepository = new UserRepository(_context);

            if(!string.IsNullOrEmpty(report.Message))
            {
                return report;
            }

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
            IUserRepository authorRepository = new UserRepository(_context);
            User user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                report.Message = $"User not found in the database!";
                return report;
            }
            
            await authorRepository.Delete(user);
            return report;
        }

        public async Task<UserModel> Read(Guid id)
        {
            IUserRepository userRepository = new UserRepository(_context);
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

        public  BaseResponseModel Update(UserModel userModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            IUserRepository userRepository = new UserRepository(_context);
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
