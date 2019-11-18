using App.DataAccessLayer.Entities.Enum;
using System.Collections.Generic;

namespace App.DataAccessLayer.Entities
{
    public class PrintingEdition : Base.Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Types Type { get; set; }
        public Cover Cover { get; set; }
        public List<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
    }
}
