using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;

using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<Author> GetById(Guid id);
        public Task<bool> Delete(Author item);

    }
}
