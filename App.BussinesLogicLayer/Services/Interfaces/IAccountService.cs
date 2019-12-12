using App.BussinesLogicLayer.Models.Response;
using App.BussinesLogicLayer.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<BaseResponseModel> RegisterAsync(SingUpModel model);
        public string CreateLink(ResetPasswordModel model, string action);
        public Task<BaseResponseModel> ForgotPasswordAsync(ResetPasswordModel model);
        public Task<BaseResponseModel> ResetPasswordAsync(ResetPasswordModel model);
        public Task<IdentityResult> CreateRoleAsync(string Role);
        public Task<BaseResponseModel> ConfirmEmailAsync(ResetPasswordModel model);
        public Task<LogInResponseModel> SingInAsync(SingInModel model);
    }
}
