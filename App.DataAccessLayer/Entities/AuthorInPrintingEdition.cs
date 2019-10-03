using System;

namespace App.DataAccessLayer.Entities
{
    public class AuthorInPrintingEdition
    {
        public Author AuthorId { get; set; }
        public PrintingEdition PrintingEditionId { get; set; }
        public DateTime Date { get; set; }
    }
}
