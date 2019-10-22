using App.BussinesLogicLayer.Helper;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUrlHelperFactory _urlHelper;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IHttpContextAccessor contextAccessor, IUrlHelperFactory urlHelper, IActionContextAccessor  actionContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _actionContextAccessor = actionContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<object> Register(UserModel model)
        {
            if(model == null)
            {
                return new ApplicationException("You passed an empty object!");
            }
            User user = new User();
            user.UserName = model.Email;
            user.Email = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);


            if (!result.Succeeded)
            {
                return new ApplicationException("UNKNOWN_ERROR");
            }

            await _signInManager.SignInAsync(user, false);
            var token = GenerateJwtToken(model.Email, user);

            await _userManager.AddToRoleAsync(user, "user");

            string code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            string confirmEmailLink= CreateLink(user.Id, code, "ConfirmEmail");
            
            EmailHelper email = new EmailHelper();
            email.SendEmail(user.Email, "ConfirmEmail", confirmEmailLink);

            return token;
            
        }

        public async Task<object> Login(UserModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();
            string result = "You have successfully logged out";
            return result;
        }

        public async Task<string> ForgotPassword(UserModel model)
        {
            User user = await _userManager.FindByNameAsync(model.Email);
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);

            string passwordEmailLink = CreateLink(user.Id, code, "ResetPassword");

            EmailHelper email = new EmailHelper();
            email.SendEmail(user.Email, "ResetPassword", passwordEmailLink);

            BaseResponseModel response = new BaseResponseModel();
            response.Message = "The email has been sent.";
            return response.Message;
        }

        public async Task<string> ResetPassword(ResetPasswordModel model)
        {
            BaseResponseModel report = new BaseResponseModel();

            User user = await _userManager.FindByNameAsync(model.Email);

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
            {
                report.Message = "You were unable to change your password!";
            }
            report.Message = "You have successfully changed your password!";
            return report.Message;
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

            var callbackUrl = _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                action,
                "Account",
                new { userId = id, code },
                protocol: _contextAccessor.HttpContext.Request.Scheme);

            return callbackUrl.ToString();
        }

        public async Task CreateRole(string name)
        {
            IdentityRole role = new IdentityRole(name);
            await _roleManager.CreateAsync(role);
        }
    }
}
