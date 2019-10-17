﻿using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<string> Delete(User item);
    }
}
