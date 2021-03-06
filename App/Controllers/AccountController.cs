﻿using App.BussinesLogicLayer;
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
        public async Task<RedirectResult> ConfirmEmailAsync(string email, string code, string url)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.Email = email;
            model.Code = code;
            await _accountService.ConfirmEmailAsync(model);

            return Redirect($"http://{url}");
        }

        [HttpGet("CreateRole")]
        public async Task<IdentityResult> CreateRoleAsync([FromQuery]string role)
        {
            IdentityResult result = await _accountService.CreateRoleAsync(role);

            return result;
        }

        [HttpPost("Register")]
        public async Task<BaseResponseModel> RegisterAsync([FromBody] SingUpModel model)
        {
            BaseResponseModel result = await _accountService.RegisterAsync(model);

            return result;
        }

        [HttpPost("ForgotPassword")]
        public async Task<BaseResponseModel> ForgotPasswordAsync([FromBody] ResetPasswordModel model)
        {
            BaseResponseModel result = await _accountService.ForgotPasswordAsync(model);

            return result;
        }

        [HttpGet("ResetPassword")]
        public async Task<RedirectResult> ResetPasswordAsync([FromQuery] ResetPasswordModel model)
        {
            await _accountService.ResetPasswordAsync(model);

            return Redirect($"http://{model.Url}");
        }

        [HttpPost("SingIn")]
        public async Task<LogInResponseModel> SingInAsync([FromBody] SingInModel model)
        {
            LogInResponseModel logInResponse = await _accountService.SingInAsync(model);

            return logInResponse;
        }
    }
}