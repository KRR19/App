﻿using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        public Task<OrderItem> Read(Guid Id);
        public Task<bool> Delete(OrderItem item);
        public OrderItem GetLast();
    }
}
