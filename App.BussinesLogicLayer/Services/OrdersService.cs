﻿using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Models.Payments;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order = App.DataAccessLayer.Entities.Order;
using OrderItem = App.DataAccessLayer.Entities.OrderItem;

namespace App.BussinesLogicLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPrintingEditionsRepository _printingEditionsRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly UserManager<User> _userManager;

        private readonly string _successDeletMsg = "You have successfully deleted";

        public OrdersService(IOrderRepository orderRepository, IPrintingEditionsRepository printingEditionsRepository,
                             IOrderItemRepository orderItemRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _printingEditionsRepository = printingEditionsRepository;
            _orderItemRepository = orderItemRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponseModel> Create(OrderModel orderModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            PaymentModel paymentModel = new PaymentModel();
            Order order = new Order();
            order.OrderItem = new List<OrderItem>();
            order.Payment = new Payment();

            paymentModel.Amount = orderModel.Amount;
            paymentModel.Currency = orderModel.Currency;
            paymentModel.Description = orderModel.Description;
            paymentModel.Email = orderModel.PaymentEmail;
            paymentModel.Source = orderModel.PaymentSource;

            order.Payment.TransactionId = Charge(paymentModel);
            order.Payment.CreationDate = DateTime.Now;

            order.CreationDate = order.Date = DateTime.Now;
            order.IsRemoved = false;
            order.Description = orderModel.Description;
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());

            foreach (var item in orderModel.PrintingEdition)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.CreationDate = DateTime.Now;
                orderItem.IsRemoved = false;
                orderItem.PrintingEdition = await _printingEditionsRepository.GetById(item.PrintingEditionId);
                orderItem.Count = item.PrintingEditionCount;
                orderItem.Currency = item.PrintingEditionCurrency;
                orderItem.Amount = item.PrintingEditionPrice * item.PrintingEditionCount;
                orderItem.Order = order;

                order.OrderItem.Add(orderItem);
            }

            await _orderRepository.Create(order);

            return report;
        }
        public List<Order> GetAll()
        {
            List<Order> order = _orderRepository.GetAll();

            return order;
        }
        public async Task<BaseResponseModel> Update(OrderModel orderModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            Order order = new Order();
            OrderItem orderItem = new OrderItem();

            order.CreationDate = order.Date = DateTime.Now;
            order.IsRemoved = false;
            order.Description = orderModel.Description;
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());
            order = await _orderRepository.Update(order);

            orderItem.CreationDate = DateTime.Now;
            orderItem.IsRemoved = false;
            orderItem.PrintingEdition = await _printingEditionsRepository.GetById(orderModel.PrintingEdition.Last().PrintingEditionId);
            orderItem.Count = orderModel.PrintingEdition.Last().PrintingEditionCount;
            orderItem.Currency = orderModel.Currency;
            orderItem.Amount = orderItem.PrintingEdition.Price * orderItem.Count;
            orderItem.Order = order;
            await _orderItemRepository.Update(orderItem);

            return report;
        }

        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            Order order = await _orderRepository.GetById(id);

            order.IsRemoved = true;
            bool result = await _orderRepository.Delete(order);

            if (result)
            {
                report.Message.Add(_successDeletMsg);
            }

            return report;
        }

        public string Charge(PaymentModel model)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = model.Email,
                Source = model.Source
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(model.Amount),
                Description = model.Description,
                Currency = model.Currency.ToString(),
                Customer = customer.Id

            });

            return charge.BalanceTransactionId;
        }
    }
}
