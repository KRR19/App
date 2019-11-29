﻿using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationContext _context;
        public OrderItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetById(Guid Id)
        {
            OrderItem orderItem = await _context.OrderItems.FindAsync(Id);

            return orderItem;
        }

        public async Task<OrderItem> Create(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<List<OrderItem>> Create(List<OrderItem> item)
        {
            await _context.OrderItems.AddRangeAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<OrderItem> Update(OrderItem item)
        {
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> Delete(OrderItem item)
        {
            bool result;
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
