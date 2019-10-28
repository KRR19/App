using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet("Get/{Id}")]
        public async Task<OrderItemModel> Get(Guid Id)
        {
            OrderItemModel author = await _service.Read(Id);

            return author;
        }

        [HttpPost("Create")]
        public async Task<OrderItemModel> Create([FromBody]OrderItemModel newOrderItem)
        {
            OrderItemModel model = await _service.Create(newOrderItem);

            return model;
        }
        [HttpPut("Update")]
        public BaseResponseModel Update([FromBody]OrderItemModel orderItemModel)
        {
            BaseResponseModel report = _service.Update(orderItemModel);

            return report;
        }

        [HttpDelete("Delete")]
        public async Task<BaseResponseModel> Delete([FromBody]OrderItemModel orderItemModel)
        {
            BaseResponseModel report = await _service.Delete(orderItemModel.Id);

            return report;
        }
    }
}