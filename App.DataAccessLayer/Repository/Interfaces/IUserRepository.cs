using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> Read(Guid Id);
        public Task<bool> Delete(User item);
    }
}
