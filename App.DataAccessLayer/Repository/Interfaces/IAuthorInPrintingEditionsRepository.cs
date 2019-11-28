using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IAuthorInPrintingEditionsRepository : IBaseRepository<AuthorInPrintingEdition>
    {
        List<Guid> GetAuthors(Guid id);
        List<string> GetAuthorsName(List<Guid> authorId);
        List<AuthorInPrintingEdition> GetById(Guid id);
    }
}
