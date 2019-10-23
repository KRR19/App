using App.DataAccessLayer.Entities.Enum;
using System;

namespace App.BussinesLogicLayer.Models.Orders
{
    public class OrderModel
    {
        public string UserName { get; set; }
        public Guid PrintingEdition { get; set; }
        public Currency Currency { get; set; }
        public int Count { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string PaymentEmail { get; set; }
        public string PaymentSource { get; set; }
    }
}
