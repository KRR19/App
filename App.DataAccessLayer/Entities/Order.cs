using App.DataAccessLayer.Entities.Base;
using System;

namespace App.DataAccessLayer.Entities
{
    public class Order : Essence
    {
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public Payment Payment { get; set; }
    }
}
