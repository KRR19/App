using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;

using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {

        public Task<string> Create(Author author);
        public Task<string> Delete(Author author);
        public string Update(Author item);
        public Task<Author> Read(Guid id);

    }
}
