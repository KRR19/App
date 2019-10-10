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
        public async Task<string> Create(OrderItem item)
        {
            string result;

            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();

            result = result = $"You add - {item.Id}";
            return result;
        }

        public async Task<string> Delete(OrderItem item)
        {
            string result;
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
            result = $"You delete {item.Id}";
            return result;
        }

        public async Task<OrderItem> Read(Guid Id)
        {
            OrderItem orderItem = await _context.OrderItems.FindAsync(Id);
            return orderItem;
        }

        public string Update(OrderItem item)
        {
            string result = $"You update {item.Id}";
            _context.OrderItems.Update(item);
            _context.SaveChanges();
            return result;
        }
    }
}
