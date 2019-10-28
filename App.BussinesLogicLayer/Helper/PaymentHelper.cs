using App.BussinesLogicLayer.Models.Payments;
using Stripe;
using System;

namespace App.BussinesLogicLayer.Helper
{
    public class PaymentHelper
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }

        
    }
}
