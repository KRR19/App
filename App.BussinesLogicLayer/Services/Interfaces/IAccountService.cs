using App.BussinesLogicLayer.Models.Users;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<object> Register(UserModel model);
        public Task<object> Login(UserModel model);
        public Task<string> LogOut();
        public string CreateLink(string id, string code, string action);
        public Task<string> ForgotPassword(UserModel model);
        public Task<string> ResetPassword(ResetPasswordModel model);
    }
}
