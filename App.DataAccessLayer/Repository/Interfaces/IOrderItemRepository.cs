﻿using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        public Task<OrderItem> GetById(Guid Id);
        public Task<bool> Delete(OrderItem item);
        public Task<List<OrderItem>> Create(List<OrderItem> item);
    }
}
