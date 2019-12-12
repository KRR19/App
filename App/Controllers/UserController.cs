using App.BussinesLogicLayer.Models.Users;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<UserInfoModel>> GetAllAsync()
        {
            List<UserInfoModel> users = await _userService.GetAllAsync();

            return users;
        }

        [HttpGet("GetAllRoles")]
        public List<RolesModel> GetAllRoles()
        {
            List<RolesModel> users = _userService.GetAllRolesAsync();

            return users;
        }

        [HttpPost("ChangeRole")]
        [Authorize(Roles = "ADMIN")]
        public async Task<RolesModel> ChangeRoleAsync(RolesModel rolesModel)
        {
            RolesModel user = await _userService.ChangeRoleAsync(rolesModel);

            return user;
        }
    }
}