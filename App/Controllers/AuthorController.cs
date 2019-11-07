using App.BussinesLogicLayer;
using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            List<Author> author =  _service.GetAll();
            return author;
        }

        [HttpGet("Get/{Id}")]
        public async Task<AuthorModel> Get(Guid Id)
        {
            AuthorModel author = await _service.GetById(Id);
            return author;
        }
        [HttpPost("Create")]
        public async Task<Author> Create([FromBody]AuthorModel newAuthor)
        {
            Author model = await _service.Create(newAuthor);
            return model;
        }

        [HttpPut]
        public AuthorModel Put([FromBody]AuthorModel newAuthor)
        {
            AuthorModel model = _service.Update(newAuthor);
            return model;
        }

        [HttpDelete]
        public async Task<BaseResponseModel> Delete([FromBody]AuthorModel newAuthor)
        {
            BaseResponseModel report = await _service.Delete(newAuthor.Id);
            return report;
        }
    }
}