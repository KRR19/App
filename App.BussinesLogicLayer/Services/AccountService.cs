using App.BussinesLogicLayer.Helper;
using App.BussinesLogicLayer.Models;
using App.BussinesLogicLayer.Models.Response;
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
        private readonly string _emailNOTConfirmedMsg = "Please confirm your mail";
        private readonly string _wrongPass = "You entered an incorrect password";
        public AccountService(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor contextAccessor, IUrlHelperFactory urlHelper, IActionContextAccessor actionContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _actionContextAccessor = actionContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<LogInResponseModel> Singin(UserModel model)
        {
            LogInResponseModel logInResponse = new LogInResponseModel();
            List<Claim> accessClaims = new List<Claim>();
            List<Claim> refreshClaims = new List<Claim>();
            string accessToken;
            string refreshToken;

            User user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                logInResponse.IsValid = false;
                logInResponse.Message.Add(_userNotFoundMsg);
                return logInResponse;
            }

            if(!user.EmailConfirmed)
            {
                logInResponse.IsValid = false;
                logInResponse.Message.Add(_emailNOTConfirmedMsg);
                return logInResponse;
            }

            var roles = await _userManager.GetRolesAsync(user);
            logInResponse.Role = roles.ToList().FirstOrDefault();

            logInResponse.User = user.NormalizedUserName;

            bool confirm = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!confirm)
            {
                logInResponse.IsValid = false;
                logInResponse.Message.Add(_wrongPass);

                return logInResponse;
            }

            accessClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            accessClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            accessClaims.Add(new Claim(ClaimTypes.Hash, user.PasswordHash));
            accessClaims.Add(new Claim(ClaimTypes.Role, logInResponse.Role));
            accessToken = GenerateJwtToken(accessClaims, 5000);
            logInResponse.accessToken = accessToken;

            refreshClaims.Add(new Claim(ClaimTypes.Authentication, accessToken));
            refreshClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            refreshToken = GenerateJwtToken(refreshClaims, 5000);
            logInResponse.refreshToken = refreshToken;



            return logInResponse;
        }

        public async Task<BaseResponseModel> Register(UserModel model)
        {
            User user = new User();
            EmailHelper email = new EmailHelper(_configuration);
            IdentityResult result = new IdentityResult();
            BaseResponseModel responseModel = new BaseResponseModel();
            ResetPasswordModel resetModel = new ResetPasswordModel();

            user.UserName = model.Email;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            string role = _roleManager.Roles.FirstOrDefault(p => p.NormalizedName == DefaultRoles.User).ToString();
            result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                responseModel.IsValid = result.Succeeded;
                responseModel.Message.Add(result.Errors.ToString());
                return responseModel;
            }

            await _userManager.AddToRoleAsync(user, role);

            resetModel.Email = model.Email;
            resetModel.Code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmEmailLink = CreateLink(resetModel, "ConfirmEmail");

            email.SendEmail(user.Email, "ConfirmEmail", confirmEmailLink);
            responseModel.IsValid = true;

            return responseModel;
        }

        public async Task<BaseResponseModel> ForgotPassword(ResetPasswordModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            model.Code = await _userManager.GeneratePasswordResetTokenAsync(user);

            string passwordEmailLink = CreateLink(model, "ResetPassword");

            EmailHelper email = new EmailHelper(_configuration);
            email.SendEmail(user.Email, "ResetPassword", passwordEmailLink);

            BaseResponseModel response = new BaseResponseModel();
            response.Message.Add(_sentMsg);

            return response;
        }

        public async Task<BaseResponseModel> ResetPassword(ResetPasswordModel model)
        {
            BaseResponseModel report = new BaseResponseModel();

            User user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.ConfirmPassword);
            if (!result.Succeeded)
            {
                report.Message.Add(_chnPassErrMsg);
            }
            report.Message.Add(_chnPassMsg);

            return report;
        }

        private string GenerateJwtToken(List<Claim> claims, int expTime)
        {


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(expTime);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }

        public string CreateLink(ResetPasswordModel model, string action)
        {
            string callbackUrl = _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext).Action(
                action,
                "Account",
                model,
                protocol: _contextAccessor.HttpContext.Request.Scheme);

            return callbackUrl;
        }

        public async Task<IdentityResult> CreateRole(string name)
        {
            IdentityRole role = new IdentityRole(name);
            IdentityResult result = await _roleManager.CreateAsync(role);

            return result;
        }

        public async Task<BaseResponseModel> ConfirmEmail(ResetPasswordModel model)
        {
            BaseResponseModel response = new BaseResponseModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
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

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, model.Code);
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
