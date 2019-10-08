using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private ApplicationContext _context;
        private IAuthorService _service;
        public AuthorController(ApplicationContext context, IAuthorService service)
        {
            _context = context;
            _service = service;
        }
        [HttpPost]
        public void Post([FromBody]AuthorModel newAuthor)
        {            
            _service.Create(newAuthor);            
        }
    }
}