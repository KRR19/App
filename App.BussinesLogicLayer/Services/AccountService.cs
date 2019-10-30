using App.BussinesLogicLayer.Helper;
using App.BussinesLogicLayer.Models;
using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace App.BussinesLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUrlHelperFactory _urlHelper;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;


        private readonly string _sentMsg = "The email has been sent.";
        private readonly string _chnPassMsg = "You have successfully changed your password!";
        private readonly string _chnPassErrMsg = "You were unable to change your password!";
        private readonly string _userNotFoundMsg = "User not found!";
        private readonly string _emailAlreadyConfirmedMsg = "This email has already been confirmed.";
        private readonly string _emailNotFoundMsg = "Email is not verified.";
        private readonly string _emailConfirmedMsg = "Email confirmed.";
        public AccountService(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor contextAccessor, IUrlHelperFactory urlHelper, IActionContextAccessor actionContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _actionContextAccessor = actionContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(UserModel model)
        {
            User user = new User();
            EmailHelper email = new EmailHelper(_configuration);
            IdentityResult result = new IdentityResult();

            user.UserName = model.Email;
            user.Email = model.Email;
            string role = _roleManager.Roles.FirstOrDefault(p => p.NormalizedName == DefaultRoles.User).ToString();
            result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return result;
            }

            await _userManager.AddToRoleAsync(user, role);

            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmEmailLink = CreateLink(user.Id, code, "ConfirmEmail");

            email.SendEmail(user.Email, "ConfirmEmail", confirmEmailLink);

            return result;
        }

        public async Task<BaseResponseModel> ForgotPassword(UserModel model)
        {
            User user = await _userManager.FindByNameAsync(model.Email);
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);

            string passwordEmailLink = CreateLink(user.Id, code, "ResetPassword");

            EmailHelper email = new EmailHelper(_configuration);
            email.SendEmail(user.Email, "ResetPassword", passwordEmailLink);

            BaseResponseModel response = new BaseResponseModel();
            response.Message.Add(_sentMsg);

            return response;
        }

        public async Task<BaseResponseModel> ResetPassword(ResetPasswordModel model)
        {
            BaseResponseModel report = new BaseResponseModel();

            User user = await _userManager.FindByNameAsync(model.Email);

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
            {
                report.Message.Add(_chnPassErrMsg);
            }
            report.Message.Add(_chnPassMsg);

            return report;
        }

        public JwtSecurityToken GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return token;
        }

        public string CreateLink(string id, string code, string action)
        {
            string callbackUrl = _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                action,
                "Account",
                new { userId = id, code },
                protocol: _contextAccessor.HttpContext.Request.Scheme);

            return callbackUrl;
        }

        public async Task<IdentityResult> CreateRole(string name)
        {
            IdentityRole role = new IdentityRole(name);
            IdentityResult result = await _roleManager.CreateAsync(role);

            return result;
        }

        public async Task<BaseResponseModel> ConfirmEmail(string userId, string code)
        {
            BaseResponseModel response = new BaseResponseModel();
            var user = await _userManager.FindByIdAsync(userId);
            if (user.EmailConfirmed)
            {
                response.Message.Add(_emailAlreadyConfirmedMsg);
                response.IsValid = false;
                return response;
            }
            if (user == null)
            {
                response.Message.Add(_userNotFoundMsg);
                response.IsValid = false;
                return response;
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                response.Message.Add(_emailNotFoundMsg);
                response.IsValid = true;
                return response;
            }

            response.Message.Add(_emailConfirmedMsg);
            response.IsValid = true;
            return response;
        }
    }
}
