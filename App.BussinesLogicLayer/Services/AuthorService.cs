using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;

namespace App.BussinesLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private ApplicationContext _context;
        public AuthorService(ApplicationContext context)
        {
            _context = context;
        }
        public string Create(AuthorModel newAuthor)
        {
            string createName;
            
            Author author = new Author();
            IAuthorRepository authorRepository = new AuthorRepository(_context);
            author.Name = newAuthor.Name;
            author.CreationData = DateTime.Now;
            author.IsRemoved = false;
            createName =  authorRepository.Create(author);
            return createName;
        }
    }
}
