using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountService _accountService;


        public AccountController(UserManager<User> userManager, IAccountService accountService)
        {
            _userManager = userManager;
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<JwtSecurityToken> Register([FromBody] UserModel model)
        {
            var token = await _accountService.Register(model);
            return token;
        }

        [HttpPost("ForgotPassword")]
        public async Task<BaseResponseModel> ForgotPassword([FromBody] UserModel model)
        {
            BaseResponseModel result = await _accountService.ForgotPassword(model);
            return result;
        }

        [HttpPost("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<BaseResponseModel> ResetPassword(ResetPasswordModel model)
        {
            BaseResponseModel result = await _accountService.ResetPassword(model);
            return result;
        }

        [HttpGet("ConfirmEmail")]
        public async Task ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, code);
        }

        [HttpGet("CreateRole")]
        public async Task<IdentityResult> CreateRole([FromQuery]string role)
        {
            IdentityResult result = await _accountService.CreateRole(role);
            return result;
        }
    }
}