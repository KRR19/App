using App.BussinesLogicLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponseModel> CreateAsync(UserModel userModel);
        public Task<BaseResponseModel> DeleteAsync(Guid id);
        public BaseResponseModel Update(UserModel userModel);
        public Task<UserModel> GetByIdAsync(Guid id);
        public Task<List<UserInfoModel>> GetAllAsync();
        public List<RolesModel> GetAllRolesAsync();
        public Task<RolesModel> ChangeRoleAsync(RolesModel rolesModel);
    }
}
