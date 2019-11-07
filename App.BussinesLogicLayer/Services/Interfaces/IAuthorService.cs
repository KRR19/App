using App.BussinesLogicLayer.models.Authors;
using App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<Author> Create(AuthorModel newAuthor);
        public Task<BaseResponseModel> Delete(Guid id);
        public AuthorModel Update(AuthorModel UpdateAuthor);
        public Task<AuthorModel> GetById(Guid id);
        public List<Author> GetAll();
    }
}
