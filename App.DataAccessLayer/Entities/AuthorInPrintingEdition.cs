using System;

namespace App.DataAccessLayer.Entities
{
    public class AuthorInPrintingEdition
    {
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public Guid PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public DateTime Date { get; set; }
    }
}
