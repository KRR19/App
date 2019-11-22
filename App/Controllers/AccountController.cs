using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.Response;
using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("ConfirmEmail")]
        public async Task<RedirectResult> ConfirmEmail(string email, string code)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.Email = email;
            model.Code = code;
            await _accountService.ConfirmEmail(model);
            return Redirect("http://localhost:4200/auth");
        }

        [HttpGet("CreateRole")]
        public async Task<IdentityResult> CreateRole([FromQuery]string role)
        {
            IdentityResult result = await _accountService.CreateRole(role);
            return result;
        }

        [HttpPost("Register")]
        public async Task<BaseResponseModel> Register([FromBody] UserModel model)
        {
            BaseResponseModel result = await _accountService.Register(model);
            return result;
        }

        [HttpPost("ForgotPassword")]
        public async Task<BaseResponseModel> ForgotPassword([FromBody] ResetPasswordModel model)
        {
            BaseResponseModel result = await _accountService.ForgotPassword(model);
            return result;
        }

        [HttpGet("ResetPassword")]
        public async Task<RedirectResult> ResetPassword([FromQuery] ResetPasswordModel model)
        {
            BaseResponseModel result = await _accountService.ResetPassword(model);
            return Redirect("http://localhost:4200/auth");
        }

        [HttpPost("SingIn")]
        public async Task<LogInResponseModel> SingIn([FromBody] SingInModel model)
        {
            LogInResponseModel logInResponse = await _accountService.Singin(model);
            return logInResponse;
        }
    }
}