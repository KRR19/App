using App.BussinesLogicLayer.Models.Users;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponseModel> Create(UserModel userModel);
        public Task<BaseResponseModel> Delete(Guid id);
        public BaseResponseModel Update(UserModel userModel);
        public Task<UserModel> GetById(Guid id);
    }
}
