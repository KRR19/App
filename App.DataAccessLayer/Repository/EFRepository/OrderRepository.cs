using App.DataAccessLayer.AppContext;
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
        public async Task<bool> Create(Order item)
        {
            bool result = true;
            await _context.Orders.AddAsync(item);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(Order item)
        {
            bool result;
            _context.Orders.Update(item);
            await _context.SaveChangesAsync();
            result = true;
            return result;
        }

        public List<Order> GetAll()
        {
            var order = _context.Orders.ToList();
            return order;
        }

        public Order GetLast()
        {
            Order order = _context.Orders.ToList<Order>().Last<Order>();
            return order;
        }


        public async Task<Order> GetById(Guid id)
        {
           Order order = await _context.Orders.FindAsync(id);
            return order;
        }

        public bool Update(Order item)
        {
            bool result=true;
            _context.Orders.Update(item);
            _context.SaveChanges();
            return result;
        }
    }
}
