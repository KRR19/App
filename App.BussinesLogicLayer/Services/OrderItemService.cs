﻿using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationContext _context;

        public OrderItemService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderItemModel> Create(OrderItemModel newOrderItem)
        {
            IOrderItemRepository orderItemRepository = new OrderItemRepository(_context);
            PrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);

            var name = _context.PrintingEditions.Where(x => x.Name == newOrderItem.PrintingEdition);

            OrderItem orderItem = new OrderItem
            {
                Amount = newOrderItem.Amount,
                Currency = newOrderItem.Currency,
                Count = newOrderItem.Count,
                CreationDate = DateTime.Now,
                PrintingEdition = name.FirstOrDefault(),
                IsRemoved = false
            };

            BaseResponseModel report = new BaseResponseModel();

            await orderItemRepository.Create(orderItem);

            return newOrderItem;
        }

        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            IOrderItemRepository orderItemRepository = new OrderItemRepository(_context);
            OrderItem orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                report.Message.Add("OrderItem not found in the database!");
                return report;
            }
            orderItem.IsRemoved = true;
            await orderItemRepository.Delete(orderItem);
            return report;
        }

        public async Task<OrderItemModel> Read(Guid id)
        {
            IOrderItemRepository orderItemRepository = new OrderItemRepository(_context);
            OrderItem orderItem = await orderItemRepository.Read(id);

            OrderItemModel orderItemModel = new OrderItemModel
            {
                Amount = orderItem.Amount,
                Count = orderItem.Count,
                Id = orderItem.Id,
                Currency = orderItem.Currency,
                PrintingEdition = orderItem.PrintingEdition.Id.ToString()
            };

            return orderItemModel;
        }

        public BaseResponseModel Update(OrderItemModel UpdateOrderItem)
        {
            IOrderItemRepository orderItemRepository = new OrderItemRepository(_context);

            var s = _context.PrintingEditions.Where(n => n.Name == UpdateOrderItem.PrintingEdition);

            OrderItem orderItem = new OrderItem
            {
                Id = UpdateOrderItem.Id,
                Amount = UpdateOrderItem.Amount,
                Count = UpdateOrderItem.Count,
                PrintingEdition = s.FirstOrDefault(),
                Currency = UpdateOrderItem.Currency
            };

            BaseResponseModel report = new BaseResponseModel();
            orderItemRepository.Update(orderItem);

            return report;
        }
    }
}
