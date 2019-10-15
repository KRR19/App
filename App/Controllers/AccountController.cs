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
            var result = _accountService.Login(model);
            return result;
        }

        [HttpPost]
        public async Task<object> Register([FromBody] UserModel model)
        {
            var token = _accountService.Register(model);
            return token;
        }





    }
}