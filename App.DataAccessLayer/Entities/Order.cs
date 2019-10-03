using App.DataAccessLayer.Entities.Base;
using System;

namespace App.DataAccessLayer.Entities
{
    public class Order : Essence
    {
        public string Description { get; set; }
        public User UserId { get; set; }
        public DateTime Date { get; set; }
        public Payment PaymentId { get; set; }
    }
}
