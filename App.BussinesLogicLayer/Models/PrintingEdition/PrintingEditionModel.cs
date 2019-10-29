using App.DataAccessLayer.Entities.Enum;
using System;

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
        public Guid Author { get; set; }

    }
}
