using App.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace App.DataAccessLayer.Entities
{
    public class Order : Base.Base
    {
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public Payment Payment { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}
