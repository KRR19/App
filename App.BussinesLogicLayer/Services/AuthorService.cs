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

        public async Task<BaseResponseModel> Create(AuthorModel newAuthor)
        {
            BaseResponseModel report = IsValidation(newAuthor);
            Author author = new Author();

            if (report.Validation)
            {
                author.Name = newAuthor.Name;
                author.DateBirth = newAuthor.DateBirth;
                author.DateDeath = newAuthor.DateDeath;
                author.CreationDate = DateTime.Now;
                author.IsRemoved = false;

                report.Message.Add(await _authorRepository.Create(author));
            }

            return report;
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
        public BaseResponseModel Update(AuthorModel UpdateAuthor)
        {
            BaseResponseModel report = IsValidation(UpdateAuthor);

            if (report.Validation)
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
                report.Message.Add(_authorRepository.Update(author));
            }

            return report;
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
        private BaseResponseModel IsValidation(AuthorModel author)
        {
            BaseResponseModel report = new BaseResponseModel();
            if (author == null)
            {
                report.Message.Add("You send NULL!");
                return report;
            }

            if (string.IsNullOrEmpty(author.Name) || string.IsNullOrWhiteSpace(author.Name))
            {
                report.Message.Add("Author name are empty!");
                return report;
            }

            if (author.DateBirth >= author.DateDeath)
            {
                report.Message.Add("Plese check the dates!");
                return report;
            }

            report.Validation = true;
            return report;
        }
    }
}
