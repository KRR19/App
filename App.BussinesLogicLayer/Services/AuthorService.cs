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

      
        public void Create(AuthorModel newAuthor)
        {

            if (newAuthor != null)
            {
                Author author = new Author();
                IAuthorRepository authorRepository = new AuthorRepository(_context);
                author.Name = newAuthor.Name;
                author.DateBirth = author.DateBirth;
                author.DateDeath = author.DateDeath;
                author.CreationData = DateTime.Now;
                author.IsRemoved = false;
                authorRepository.Create(author);
            }       
        }
    }
}
