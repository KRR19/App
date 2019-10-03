using App.DataAccessLayer.Entities.Base;
using App.DataAccessLayer.Entities.Enum;

namespace App.DataAccessLayer.Entities
{
    public class PrintingEdition : Essence
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Types Type { get; set; }


    }
}
