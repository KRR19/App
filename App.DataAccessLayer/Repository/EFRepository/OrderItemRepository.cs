using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationContext _context;
        public OrderItemRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(OrderItem item)
        {
            bool result;

            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<bool> Delete(OrderItem item)
        {
            bool result;
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
            result =true;
            return result;
        }

        public async Task<OrderItem> Read(Guid Id)
        {
            OrderItem orderItem = await _context.OrderItems.FindAsync(Id);
            return orderItem;
        }

        public bool Update(OrderItem item)
        {
            bool result;
            _context.OrderItems.Update(item);
            _context.SaveChanges();
            result = true;
            return result;
        }
    }
}
