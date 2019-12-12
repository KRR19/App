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
        public async Task<BaseResponseModel> CreateAsync([FromBody]OrderModel orderModel)
        {
            BaseResponseModel report = await _service.CreateAsync(orderModel);
            return report;
        }

        [HttpPost("Update")]
        public async Task<BaseResponseModel> UpdateAsync([FromBody]OrderModel orderModel)
        {
            BaseResponseModel report = await _service.UpdateAsync(orderModel);
            return report;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponseModel> DeleteAsync([FromBody]Guid id)
        {
            BaseResponseModel report = await _service.DeleteAsync(id);
            return report;
        }
    }
}