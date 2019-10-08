using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace App.BussinesLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private ApplicationContext _context;
        

        public AuthorService(ApplicationContext context)
        {
            _context = context;            
        }

      
        public async Task<BaseResponseModel> Create(AuthorModel newAuthor)
        {
            BaseResponseModel report = new BaseResponseModel();
            IAuthorRepository authorRepository = new AuthorRepository(_context);

            report =  Validation(newAuthor);

            if (string.IsNullOrEmpty(report.Message))
            {
                Author author = new Author();
                author.Name = newAuthor.Name;
                author.DateBirth = newAuthor.DateBirth;
                author.DateDeath = newAuthor.DateDeath;
                author.CreationData = DateTime.Now;
                author.IsRemoved = false;
                report.Message = await authorRepository.Create(author);
            }  

            return report;
        }

        private BaseResponseModel Validation(AuthorModel author)
        {
            BaseResponseModel report = new BaseResponseModel();
            if(author == null)
            {
                report.Message = "You send NULL!";
                return report;
            }

            if(string.IsNullOrEmpty(author.Name) ||string.IsNullOrWhiteSpace(author.Name))
            {
                report.Message = "Author name are empty!";
                return report;
            }

            if(author.DateBirth>=author.DateDeath)
            {
                report.Message = "Plese check the dates!";
                return report;
            }

            var x = _context.Authors.Where(x => x.Name == author.Name).Select(x => x.Id).ToList();
            if(x.Count != 0)
            {
                report.Message = "This author already exists.";
                return report;
            }
            

            return report;

        }
    }
}
