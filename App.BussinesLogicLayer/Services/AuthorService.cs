using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Repository.EFRepository;
using System;

namespace App.BussinesLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private ApplicationContext DB;
        public AuthorService(ApplicationContext context)
        {
            DB = context;
        }
        public void Create(string newAuthorName)
        {
            AuthorRepository author = new AuthorRepository(DB);
            author.Create(newAuthorName);
        }
    }
}
