using App.DataAccessLayer.Entities.Enum;
using System;

namespace App.BussinesLogicLayer.Models.Orders
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public string PrintingEdition { get; set; }
        public int Count { get; set; }
    }
}
