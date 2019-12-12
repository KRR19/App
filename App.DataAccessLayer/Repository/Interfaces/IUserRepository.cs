using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> ReadAsync(Guid Id);
        public Task<bool> Delete(User item);
    }
}
