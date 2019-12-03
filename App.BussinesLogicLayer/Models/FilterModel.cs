using System;
using System.Collections.Generic;
using System.Text;

namespace App.BussinesLogicLayer.Models
{
    public class FilterModel
    {
        public List<Guid> AuthorId { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }
    }
}
