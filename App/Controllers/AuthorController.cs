using App.BussinesLogicLayer;
using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.BussinesLogicLayer.Models;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;
        public AuthorController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public List<Author> GetAll()
        {
            List<Author> author = _service.GetAll();

            return author;
        }

        [HttpGet("Get/{Id}")]
        public async Task<AuthorModel> GetAsync(Guid Id)
        {
            AuthorModel author = await _service.GetByIdAsync(Id);

            return author;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<Author> CreateAsync([FromBody]AuthorModel newAuthor)
        {
            Author model = await _service.CreateAsync(newAuthor);

            return model;
        }

        [HttpPost("Update")]
        [Authorize(Roles = "ADMIN")]
        public AuthorModel Put([FromBody]AuthorModel newAuthor)
        {
            AuthorModel model = _service.Update(newAuthor);

            return model;
        }

        [HttpPost("Delete")]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponseModel> DeleteAsync([FromBody]AuthorModel newAuthor)
        {
            BaseResponseModel report = await _service.DeleteAsync(newAuthor.Id);

            return report;
        }
    }
}