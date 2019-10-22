using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Stripe;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _service;

     

        public OrderItemController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<BaseResponseModel> Post([FromBody]OrderItemModel newOrderItem)
        {
            BaseResponseModel report = await _service.Create(newOrderItem);

            return report;
        }

        [HttpDelete]
        public async Task<BaseResponseModel> Delete([FromBody]OrderItemModel orderItemModel)
        {
            BaseResponseModel report = await _service.Delete(orderItemModel.Id);

            return report;
        }
       

        [HttpPut]
        public BaseResponseModel Put([FromBody]OrderItemModel orderItemModel)
        {
            BaseResponseModel report = _service.Update(orderItemModel);

            return report;
        }

        [HttpGet]
        public async Task<OrderItemModel> Get(Guid Id)
        {
            OrderItemModel author = await _service.Read(Id);

            return author;
        }
    }
}