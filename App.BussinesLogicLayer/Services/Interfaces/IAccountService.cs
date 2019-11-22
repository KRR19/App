using App.BussinesLogicLayer.Models.Response;
using App.BussinesLogicLayer.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<BaseResponseModel> Register(UserModel model);
        public string CreateLink(ResetPasswordModel model, string action);
        public Task<BaseResponseModel> ForgotPassword(ResetPasswordModel model);
        public Task<BaseResponseModel> ResetPassword(ResetPasswordModel model);
        public Task<IdentityResult> CreateRole(string Role);
        public Task<BaseResponseModel> ConfirmEmail(ResetPasswordModel model);
        public Task<LogInResponseModel> Singin(SingInModel model);
    }
}
