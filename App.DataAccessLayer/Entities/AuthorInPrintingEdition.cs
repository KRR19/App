using System;
using System.ComponentModel.DataAnnotations.Schema;
using App.DataAccessLayer.Entities.Base;

namespace App.DataAccessLayer.Entities
{
    public class AuthorInPrintingEdition
    {
        
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        
        public DateTime Date { get; set; }
    }
}
