using App.DataAccessLayer.Entities.Enum;
using System;
using System.Collections.Generic;

namespace App.BussinesLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Types Type { get; set; }
        public List<Guid> AuthorId { get; set; }
        public List<string> AuthorName { get; set; }
        public string Image { get; set; }

    }
}
