using App.DataAccessLayer.Entities.Enum;

namespace App.BussinesLogicLayer.Models.Payments
{
    public class PaymentModel
    {
        public string Email { get; set; }
        public string Source { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
    }
}
