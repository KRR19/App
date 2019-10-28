using App.BussinesLogicLayer.models.Authors;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorModel> Create(AuthorModel newAuthor);
        public Task<BaseResponseModel> Delete(Guid id);
        public AuthorModel Update(AuthorModel UpdateAuthor);
        public Task<AuthorModel> GetById(Guid id);
    }
}
