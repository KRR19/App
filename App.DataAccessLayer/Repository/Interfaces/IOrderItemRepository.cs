using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        public Task<OrderItem> GetByIdAsync(Guid Id);
        public Task<List<OrderItem>> CreateAsync(List<OrderItem> item);
        public Task<bool> DeleteAsync(OrderItem item);
        
    }
}
