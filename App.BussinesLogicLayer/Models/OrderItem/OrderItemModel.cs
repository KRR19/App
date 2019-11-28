using App.DataAccessLayer.Entities.Enum;
using System;

namespace App.BussinesLogicLayer.Models.Orders
{
    public class OrderItemModel
    {
        public Guid PrintingEditionId { get; set; }
        public double PrintingEditionPrice { get; set; }
        public Currency PrintingEditionCurrency { get; set; }
        public string PrintingEditionName { get; set; }
        public int PrintingEditionCount { get; set; }
    }
}
