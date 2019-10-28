using App.BussinesLogicLayer.Models.Orders;
using App.BussinesLogicLayer.Models.Payments;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Order = App.DataAccessLayer.Entities.Order;
using OrderItem = App.DataAccessLayer.Entities.OrderItem;

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
            Payment payment = new Payment();
            OrderItem orderItem = new OrderItem();
            PaymentModel paymentModel = new PaymentModel();

            paymentModel.Amount = orderModel.Amount;
            paymentModel.Currency = orderModel.Currency;
            paymentModel.Description = orderModel.Description;
            paymentModel.Email = orderModel.PaymentEmail;
            paymentModel.Source = orderModel.PaymentSource;

            payment.TransactionId = Charge(paymentModel);
            payment.CreationDate = DateTime.Now;
            await _paymentRepository.Create(payment);

            order.CreationDate = order.Date = DateTime.Now;
            order.IsRemoved = false;
            order.Description = orderModel.Description;
            order.Payment = payment;
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());
            await _orderRepository.Create(order);

            orderItem.CreationDate = DateTime.Now;
            orderItem.IsRemoved = false;
            orderItem.PrintingEdition = await _printingEditionsRepository.GetById(orderModel.PrintingEdition);
            orderItem.Count = orderModel.Count;
            orderItem.Currency = orderModel.Currency;
            orderItem.Amount = orderItem.PrintingEdition.Price * orderItem.Count;
            orderItem.Order = order;
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
            order.User = await _userManager.FindByEmailAsync(orderModel.UserName.ToString());
            order = await _orderRepository.Update(order);

            orderItem.CreationDate = DateTime.Now;
            orderItem.IsRemoved = false;
            orderItem.PrintingEdition = await _printingEditionsRepository.GetById(orderModel.PrintingEdition);
            orderItem.Count = orderModel.Count;
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
                report.Message.Add("You have successfully deleted");
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
