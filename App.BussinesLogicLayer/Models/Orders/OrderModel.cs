using App.DataAccessLayer.Entities.Enum;
using System;
using System.Collections.Generic;

namespace App.BussinesLogicLayer.Models.Orders
{
    public class OrderModel
    {
        public string UserName { get; set; }
        public OrderItemModel[] PrintingEdition { get; set; }
        public Currency Currency { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string PaymentEmail { get; set; }
        public string PaymentSource { get; set; }
    }
}
