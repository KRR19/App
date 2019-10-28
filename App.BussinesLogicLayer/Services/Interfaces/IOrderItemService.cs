using App.BussinesLogicLayer.Models.Orders;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IOrderItemService
    {
        public Task<OrderItemModel> Create(OrderItemModel newOrderItem);
        public Task<BaseResponseModel> Delete(Guid id);
        public BaseResponseModel Update(OrderItemModel UpdateOrderItem);
        public Task<OrderItemModel> Read(Guid id);
    }
}
