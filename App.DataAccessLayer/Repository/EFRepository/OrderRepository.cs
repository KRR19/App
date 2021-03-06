﻿using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            var order = _context.Orders.ToList();

            return order;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            Order order = await _context.Orders.FindAsync(id);

            return order;
        }

        public async Task<Order> CreateAsync(Order item)
        {
            await _context.Orders.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Order> UpdateAsync(Order item)
        {
            _context.Orders.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Order item)
        {
            bool result;
            _context.Orders.Update(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
