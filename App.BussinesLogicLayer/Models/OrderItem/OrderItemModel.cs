using App.DataAccessLayer.Entities.Enum;
using System;

namespace App.BussinesLogicLayer.Models.Orders
{
    public class OrderItemModel
    {
        public Guid printingEditionId { get; set; }
        public double printingEditionPrice { get; set; }
        public Currency printingEditionCurrency { get; set; }
        public string printingEditionName { get; set; }
        public int printingEditionCount { get; set; }
    }
}
