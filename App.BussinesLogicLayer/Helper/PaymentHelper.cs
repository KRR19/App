using App.BussinesLogicLayer.Models.Payments;
using Stripe;
using System;

namespace App.BussinesLogicLayer.Helper
{
    public class PaymentHelper
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }

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
