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
            BaseResponseModel report = AuthorValidation(newAuthor);
            Author author = new Author();
            bool isCreate = false;
            if (report.Validation)
            {
                author.Name = newAuthor.Name;
                author.DateBirth = newAuthor.DateBirth;
                author.DateDeath = newAuthor.DateDeath;
                author.CreationData = DateTime.Now;
                author.IsRemoved = false;

                isCreate = await _authorRepository.Create(author);
            }

            if (!isCreate)
            {
                report.Message = $"{author.Name} has been create";
            }

            return report;
        }
        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            Author author = await _authorRepository.GetById(id);

            if (author == null)
            {
                report.Message = $"Author not found in the database!";
                return report;
            }
            author.IsRemoved = true;
            bool isDelete = await _authorRepository.Delete(author);
            if (isDelete)
            {
                report.Message = "The author has been deleted";
            }
            return report;
        }

        public BaseResponseModel Update(AuthorModel UpdateAuthor)
        {
            BaseResponseModel report = AuthorValidation(UpdateAuthor);

            if (report.Validation)
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
                bool isUpdate = _authorRepository.Update(author);

                if (!isUpdate)
                {
                    report.Message = $"Failed to change {author.Name}";
                }
                report.Message = $"{author.Name} was changed";
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




        private BaseResponseModel AuthorValidation(AuthorModel author)
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

            report.Validation = true;
            return report;

        }
    }
}
