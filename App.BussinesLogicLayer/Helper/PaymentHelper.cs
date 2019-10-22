using Stripe;

namespace App.BussinesLogicLayer.Helper
{
    public class PaymentHelper
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }

        public string Charge(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = 10000,
                Description = "You Bay!",
                Currency = "usd",
                Customer = customer.Id

            });

            return charge.BalanceTransactionId;
        }
    }
}
