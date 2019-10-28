using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<Order> GetById(Guid id);
        public List<Order> GetAll();
        public Task<bool> Delete(Order item);
    }
}
