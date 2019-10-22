using App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<bool> Create(Payment payment);
        public List<Payment> GetAll();
        public Task<Payment> GetById(Guid id);
        public Payment GetLast();
    }
}
