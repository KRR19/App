using App.BussinesLogicLayer;
using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpPost]

        public async Task<BaseResponseModel> Post([FromBody]AuthorModel newAuthor)
        {
            BaseResponseModel report = await _service.Create(newAuthor);

            return report;
        }

        [HttpDelete]
        public async Task<BaseResponseModel> Delete([FromBody]AuthorModel newAuthor)
        {
            BaseResponseModel report = await _service.Delete(newAuthor.Id);

            return report;
        }

        [HttpPut]
        public BaseResponseModel Put([FromBody]AuthorModel newAuthor)
        {
            BaseResponseModel report = _service.Update(newAuthor);

            return report;
        }

        [HttpGet]
        public async Task<AuthorModel> Get(Guid Id)
        {
            AuthorModel author = await _service.GetById(Id);

            return author;
        }
    }
}