using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAll")]
        public async Task<List<UserInfoModel>>GetAll()
        {
            List<UserInfoModel> users = await _userService.GetAll();

            return users;
        }
        [HttpGet("GetAllRoles")]
        public List<RolesModel> GetAllRoles()
        {
            List<RolesModel> users = _userService.GetAllRoles();

            return users;
        }

        [HttpPost("ChangeRole")]
        [Authorize]
        public async Task<RolesModel> ChangeRole(RolesModel rolesModel)
        {
             RolesModel user = await _userService.ChangeRole(rolesModel);

            return user;
        }
    }
}