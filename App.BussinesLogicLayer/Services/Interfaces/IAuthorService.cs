using App.BussinesLogicLayer.models.Authors;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<BaseResponseModel> Create(AuthorModel newAuthor);
    }
}
