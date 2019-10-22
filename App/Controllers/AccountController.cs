using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<string> Login([FromBody] UserModel model)
        {
            var token = await _accountService.Login(model);
            return token.ToString();
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] UserModel model)
        {
            var token = await _accountService.Register(model);
            return token;
        }


        [HttpPost]
        public async Task<string> ForgotPassword([FromBody] UserModel model)
        {
            string result = await _accountService.ForgotPassword(model);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> ResetPassword(ResetPasswordModel model)
        {
            string result = await _accountService.ResetPassword(model);
            return result;
        }

        [HttpGet]
        public async Task ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, code);
        }

        [HttpPost]
        public async Task<string> Logout()
        {
            string result = await _accountService.LogOut();
            return result;
        }

        [HttpGet("CreateRole")]
        public async Task CreateRole([FromQuery]string role)
        {
            await _accountService.CreateRole(role);
        }


    }
}