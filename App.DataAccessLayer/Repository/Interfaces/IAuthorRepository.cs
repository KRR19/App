using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public string Create(Author author);
    }
}
