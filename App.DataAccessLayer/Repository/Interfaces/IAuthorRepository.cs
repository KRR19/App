using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public List<Author> GetAll();
        public Task<bool> DeleteAsync(Author item);
        public Task<Author> GetByIdAsync(Guid id);
    }
}
