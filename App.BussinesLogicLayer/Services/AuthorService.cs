using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationContext _context;

        public AuthorService(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<BaseResponseModel> Create(AuthorModel newAuthor)
        {
            BaseResponseModel report = Validation(newAuthor);
            IAuthorRepository authorRepository = new AuthorRepository(_context);
                        
            if (string.IsNullOrEmpty(report.Message))
            {
                Author author = new Author
                {
                    Name = newAuthor.Name,
                    DateBirth = newAuthor.DateBirth,
                    DateDeath = newAuthor.DateDeath,
                    CreationData = DateTime.Now,
                    IsRemoved = false
                };
                report.Message = await authorRepository.Create(author);
            }

            return report;
        }
        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            IAuthorRepository authorRepository = new AuthorRepository(_context);
            Author author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                report.Message = $"Author not found in the database!";
                return report;
            }
            author.IsRemoved = true;
            report.Message = await authorRepository.Delete(author);
            return report;
        }

        public BaseResponseModel Update(AuthorModel UpdateAuthor)
        {
            BaseResponseModel report = Validation(UpdateAuthor);
            IAuthorRepository authorRepository = new AuthorRepository(_context);

            if (string.IsNullOrEmpty(report.Message))
            {
                Author author = new Author
                {
                    Id = UpdateAuthor.Id,
                    Name = UpdateAuthor.Name,
                    DateBirth = UpdateAuthor.DateBirth,
                    DateDeath = UpdateAuthor.DateDeath,
                    CreationData = DateTime.Now,
                    IsRemoved = false
                };
                report.Message = authorRepository.Update(author);
            }

            return report;
        }

        public async Task<AuthorModel> Read(Guid id)
        {
            IAuthorRepository authorRepository = new AuthorRepository(_context);
            AuthorModel authorModel = new AuthorModel();
            Author author = await authorRepository.Read(id);

            authorModel.Id = author.Id;
            authorModel.Name = author.Name;
            authorModel.DateBirth = author.DateBirth;
            authorModel.DateDeath = author.DateDeath;
            return authorModel;
        }
    

        private BaseResponseModel Validation(AuthorModel author)
        {
            BaseResponseModel report = new BaseResponseModel();
            if (author == null)
            {
                report.Message = "You send NULL!";
                return report;
            }

            if (string.IsNullOrEmpty(author.Name) || string.IsNullOrWhiteSpace(author.Name))
            {
                report.Message = "Author name are empty!";
                return report;
            }

            if (author.DateBirth >= author.DateDeath)
            {
                report.Message = "Plese check the dates!";
                return report;
            }

            return report;

        }
    }
}
