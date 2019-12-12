using App.BussinesLogicLayer.Models.Payments;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BussinesLogic.Services.Interfaces
{
    public interface IPaymentService
    {
        public string Pay(PaymentModel model);
    }
}
