﻿using App.BussinesLogicLayer;
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
        public async Task<RedirectResult> ConfirmEmail(string userId, string code)
        {
            BaseResponseModel response = await _accountService.ConfirmEmail(userId, code);
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
    }
}