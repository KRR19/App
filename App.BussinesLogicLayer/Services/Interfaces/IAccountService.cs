using App.BussinesLogicLayer.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(UserModel model);
        public string CreateLink(string id, string code, string action);
        public Task<BaseResponseModel> ForgotPassword(UserModel model);
        public Task<BaseResponseModel> ResetPassword(ResetPasswordModel model);
        public Task<IdentityResult> CreateRole(string Role);
    }
}
