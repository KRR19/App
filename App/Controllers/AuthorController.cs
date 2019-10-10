using App.BussinesLogicLayer;
using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _service;


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
        public async Task<AuthorModel> Get([FromBody]AuthorModel newAuthor)
        {
            AuthorModel author = await _service.Read(newAuthor.Id);

            return author;
        }
    }
}