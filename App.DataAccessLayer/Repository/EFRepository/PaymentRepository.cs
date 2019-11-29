using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationContext _context;
        public PaymentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Payment> GetAll()
        {
            List<Payment> payment = _context.Payments.ToList();

            return payment;
        }
        public async Task<Payment> GetById(Guid id)
        {
            Payment payment = await _context.Payments.FindAsync(id);

            return payment;
        }
        public Payment GetLast()
        {
            Payment payment = _context.Payments.ToList<Payment>().Last<Payment>();

            return payment;
        }

        public async Task<Payment> Create(Payment payment)
        {
            EntityEntry<Payment> createdPayment = await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return createdPayment.Entity;
        }
    }
}
