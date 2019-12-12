using App.DataAccessLayer.AppContext;
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

        public async Task<OrderItem> GetByIdAsync(Guid Id)
        {
            OrderItem orderItem = await _context.OrderItems.FindAsync(Id);

            return orderItem;
        }

        public async Task<OrderItem> CreateAsync(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<List<OrderItem>> CreateAsync(List<OrderItem> item)
        {
            await _context.OrderItems.AddRangeAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<OrderItem> UpdateAsync(OrderItem item)
        {
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(OrderItem item)
        {
            bool result;
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
