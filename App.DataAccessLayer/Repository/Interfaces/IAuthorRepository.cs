using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public  Task<string> Create(Author author);
    }
}
