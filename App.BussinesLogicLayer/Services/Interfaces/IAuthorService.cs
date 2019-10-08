using App.BussinesLogicLayer.models.Authors;


namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public void Create(AuthorModel newAuthor);
    }
}
