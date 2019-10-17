
using App.DataAccessLayer.Entities.Base;
using App.DataAccessLayer.Entities.Enum;

namespace App.DataAccessLayer.Entities
{
    public class OrderItem : Base.Base
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public int Count { get; set; }
    }
}
