using App.BussinesLogicLayer.Models.Orders;
using App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IOrdersService
    {
        public Task<BaseResponseModel> CreateAsync(OrderModel orderModel);
        public List<Order> GetAll();
        public Task<BaseResponseModel> UpdateAsync(OrderModel orderModel);
        public Task<BaseResponseModel> DeleteAsync(Guid id);
    }
}
