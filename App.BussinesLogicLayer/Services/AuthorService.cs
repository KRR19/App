using App.BussinesLogicLayer.models.Authors;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository AuthorRepository)
        {
            _authorRepository = AuthorRepository;
        }

        public async Task<AuthorModel> Create(AuthorModel newAuthor)
        {
            BaseResponseModel report = ValidationAuthor(newAuthor);
            Author author = new Author();

            if (report.IsValidation)
            {
                author.Name = newAuthor.Name;
                author.DateBirth = newAuthor.DateBirth;
                author.DateDeath = newAuthor.DateDeath;
                author.CreationDate = DateTime.Now;
                author.IsRemoved = false;

                await _authorRepository.Create(author);
            }

            return newAuthor;
        }
        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            Author author = await _authorRepository.GetById(id);

            if (author == null)
            {
                report.Message.Add($"Author not found in the database!");
                return report;
            }
            author.IsRemoved = true;
            bool isDelete = await _authorRepository.Delete(author);
            if (isDelete)
            {
                report.Message.Add("The author has been deleted");
            }
            return report;
        }
        public AuthorModel Update(AuthorModel UpdateAuthor)
        {
            BaseResponseModel report = ValidationAuthor(UpdateAuthor);

            if (report.IsValidation)
            {
                Author author = new Author
                {
                    Id = UpdateAuthor.Id,
                    Name = UpdateAuthor.Name,
                    DateBirth = UpdateAuthor.DateBirth,
                    DateDeath = UpdateAuthor.DateDeath,
                    CreationDate = DateTime.Now,
                    IsRemoved = false
                };
                _authorRepository.Update(author);
            }

            return UpdateAuthor;
        }
        public async Task<AuthorModel> GetById(Guid id)
        {
            AuthorModel authorModel = new AuthorModel();
            Author author = await _authorRepository.GetById(id);

            authorModel.Id = author.Id;
            authorModel.Name = author.Name;
            authorModel.DateBirth = author.DateBirth;
            authorModel.DateDeath = author.DateDeath;
            return authorModel;
        }
        private BaseResponseModel ValidationAuthor(AuthorModel author)
        {
            BaseResponseModel report = new BaseResponseModel();
            if (author == null)
            {
                report.Message.Add("Author model is empty.");
                return report;
            }

            if (string.IsNullOrEmpty(author.Name) || string.IsNullOrWhiteSpace(author.Name))
            {
                report.Message.Add("Author name is empty.");
                return report;
            }

            if (author.DateBirth >= author.DateDeath)
            {
                report.Message.Add("Invalid dates range.");
                return report;
            }

            report.IsValidation = true;
            return report;
        }
    }
}
