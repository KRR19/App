using App.BussinesLogicLayer.Helper;
using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            this._accountService = accountService;
        }

        [HttpPost]        
        public async Task<object> Login([FromBody] UserModel model)
        {
            var token = await _accountService.Login(model);

           

            return token;
        }

        [HttpPost]
        public async Task<object> Register([FromBody] UserModel model)
        {
            EmailHelper emailService = new EmailHelper();
            var token = await _accountService.Register(model);

            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
            var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            emailService.SendEmail(user.Email, "Confirm your account", $"Confirm registration by clicking on the link: <a href='{callbackUrl}'>link</a>");

            return token;
        }

        [HttpGet]
        public async Task ConfirmEmail(string userId, string code)
        {
            
            var user = await _userManager.FindByIdAsync(userId);            
            var result = await _userManager.ConfirmEmailAsync(user, code);
           
        }

        [HttpPost]
        public async Task<string> Logout()
        {
            string result = await _accountService.LogOut();
            return result;
        }





    }
}