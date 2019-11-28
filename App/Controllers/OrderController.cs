using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _service;
        public OrderController(IOrdersService service)
        {
            _service = service;
        }

        [HttpGet("Get")]
        public List<Order> Get()
        {
            List<Order> order = _service.GetAll();
            return order;
        }

        [HttpPost("Create")]
        public async Task<BaseResponseModel> Create([FromBody]OrderModel orderModel)
        {
            BaseResponseModel report = await _service.Create(orderModel);
            return report;
        }

        [HttpPost("Update")]
        public async Task<BaseResponseModel> Update([FromBody]OrderModel orderModel)
        {
            BaseResponseModel report = await _service.Update(orderModel);
            return report;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponseModel> Delete([FromBody]Guid id)
        {
            BaseResponseModel report = await _service.Delete(id);
            return report;
        }
    }
}