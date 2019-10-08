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
        public AuthorController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpPost]
        public string Post([FromBody]AuthorModel newAuthor)
        {
            if (newAuthor == null)
            {
                return "You send NULL!!!";
            }

            string createName;
            IAuthorService authorService = new AuthorService(_context);
            createName = authorService.Create(newAuthor);
            return $"You add new author - {createName}";
        }
    }
}