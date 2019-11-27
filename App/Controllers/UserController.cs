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
        public async Task<List<UserInfoModel>> GetAll()
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