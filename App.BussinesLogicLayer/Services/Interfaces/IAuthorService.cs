using App.BussinesLogicLayer.models.Authors;
using System.Threading.Tasks;
using System;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<BaseResponseModel> Create(AuthorModel newAuthor);
        public  Task<BaseResponseModel> Delete(Guid id);
        public BaseResponseModel Update(AuthorModel UpdateAuthor);
        public Task<AuthorModel> GetById(Guid id);
    }
}
