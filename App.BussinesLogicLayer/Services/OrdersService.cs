﻿using App.BussinesLogicLayer.Helper;
using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Models.Payments;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPrintingEditionsRepository _printingEditionsRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        private readonly UserManager<User> _userManager;

        public OrdersService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IPrintingEditionsRepository printingEditionsRepository,
                             IOrderItemRepository orderItemRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _printingEditionsRepository = printingEditionsRepository;
            _orderItemRepository = orderItemRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponseModel> Create(OrderModel orderModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            Order order = new Order();
            PaymentHelper paymentHelper = new PaymentHelper();
            Payment payment = new Payment();
            OrderItem orderItem = new OrderItem();
            PaymentModel paymentModel = new PaymentModel();

            paymentModel.Amount = orderModel.Amount;
            paymentModel.Currency = orderModel.Currency;
            paymentModel.Description = orderModel.Description;
            paymentModel.Email = orderModel.PaymentEmail;
            paymentModel.Source = orderModel.PaymentSource;


            payment.TransactionId = paymentHelper.Charge(paymentModel);
            payment.CreationDate = DateTime.Now;
            await _paymentRepository.Create(payment);

            order.CreationDate = order.Date = DateTime.Now;
            order.IsRemoved = false;
            order.Description = orderModel.Description;
            order.Payment = _paymentRepository.GetLast();
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());
            await _orderRepository.Create(order);

            orderItem.CreationDate = DateTime.Now;
            orderItem.IsRemoved = false;
            orderItem.PrintingEdition = await _printingEditionsRepository.GetById(orderModel.PrintingEdition);
            orderItem.Count = orderModel.Count;
            orderItem.Currency = orderModel.Currency;
            orderItem.Amount = orderItem.PrintingEdition.Price * orderItem.Count;
            orderItem.Order = _orderRepository.GetLast();
            await _orderItemRepository.Create(orderItem);

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
            order.Payment = _paymentRepository.GetLast();
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());
            _orderRepository.Update(order);

            orderItem.CreationDate = DateTime.Now;
            orderItem.IsRemoved = false;
            orderItem.PrintingEdition = await _printingEditionsRepository.GetById(orderModel.PrintingEdition);
            orderItem.Count = orderModel.Count;
            orderItem.Currency = orderModel.Currency;
            orderItem.Amount = orderItem.PrintingEdition.Price * orderItem.Count;
            orderItem.Order = _orderRepository.GetLast();
            _orderItemRepository.Update(orderItem);

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
                report.Message.Add("You have successfully deleted");
            }

            return report;
        }
    }
}
