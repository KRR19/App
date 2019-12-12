using App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        public List<Payment> GetAll();
        public Task<Payment> GetByIdAsync(Guid id);
        public Task<Payment> CreateAsync(Payment payment);
    }
}
