using App.BussinesLogic.Services.Interfaces;
using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Models.Payments;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;

        private readonly string _successDeletMsg = "You have successfully deleted";

        public OrdersService(IOrderRepository orderRepository, IPrintingEditionsRepository printingEditionsRepository,
                             IOrderItemRepository orderItemRepository, UserManager<User> userManager, IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _printingEditionsRepository = printingEditionsRepository;
            _orderItemRepository = orderItemRepository;
            _userManager = userManager;
            _paymentService = paymentService;
        }

        public List<Order> GetAll()
        {
            List<Order> order = _orderRepository.GetAll();

            return order;
        }

        public async Task<BaseResponseModel> CreateAsync(OrderModel orderModel)
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

            order.Payment.TransactionId = _paymentService.Pay(paymentModel);
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
                orderItem.PrintingEdition = await _printingEditionsRepository.GetByIdAsync(item.PrintingEditionId);
                orderItem.Count = item.PrintingEditionCount;
                orderItem.Currency = item.PrintingEditionCurrency;
                orderItem.Amount = item.PrintingEditionPrice * item.PrintingEditionCount;
                orderItem.Order = order;

                order.OrderItem.Add(orderItem);
            }

            await _orderRepository.CreateAsync(order);

            return report;
        }

        public async Task<BaseResponseModel> UpdateAsync(OrderModel orderModel)
        {
            BaseResponseModel report = new BaseResponseModel();
            Order order = new Order();
            OrderItem orderItem = new OrderItem();

            order.CreationDate = order.Date = DateTime.Now;
            order.IsRemoved = false;
            order.Description = orderModel.Description;
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());
            order = await _orderRepository.UpdateAsync(order);

            orderItem.CreationDate = DateTime.Now;
            orderItem.IsRemoved = false;
            orderItem.PrintingEdition = await _printingEditionsRepository.GetByIdAsync(orderModel.PrintingEdition.Last().PrintingEditionId);
            orderItem.Count = orderModel.PrintingEdition.Last().PrintingEditionCount;
            orderItem.Currency = orderModel.Currency;
            orderItem.Amount = orderItem.PrintingEdition.Price * orderItem.Count;
            orderItem.Order = order;
            await _orderItemRepository.UpdateAsync(orderItem);

            return report;
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            Order order = await _orderRepository.GetByIdAsync(id);

            order.IsRemoved = true;
            bool result = await _orderRepository.DeleteAsync(order);

            if (result)
            {
                report.Message.Add(_successDeletMsg);
            }

            return report;
        }
    }
}
