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


        public AuthorController(ApplicationContext context, IAuthorService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<BaseResponseModel> Post([FromBody]AuthorModel newAuthor)
        {
            BaseResponseModel report = await _service.Create(newAuthor);

            return report;
        }
    }
}