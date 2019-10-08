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
        private ApplicationContext Context;
        public AuthorController(ApplicationContext context)
        {
            this.Context = context;
        }

        
        public string Post(string name)
        {
           AuthorService authorService = new AuthorService(Context);
           authorService.Create(name);
           return "You add " + name;
        }
    }
}