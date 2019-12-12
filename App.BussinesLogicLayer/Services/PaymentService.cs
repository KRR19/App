using App.BussinesLogic.Services.Interfaces;
using App.BussinesLogicLayer.Models.Payments;
using System;
using Stripe;
using System.Collections.Generic;
using System.Text;

namespace App.BussinesLogic.Services
{
    public class PaymentService : IPaymentService
    {
        public string Pay(PaymentModel model)
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
