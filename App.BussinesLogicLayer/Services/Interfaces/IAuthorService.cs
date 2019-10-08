using App.BussinesLogicLayer.models.Authors;


namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        public string Create(AuthorModel newAuthor);
    }
}
