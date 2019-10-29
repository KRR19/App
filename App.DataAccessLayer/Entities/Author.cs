using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.DataAccessLayer.Entities
{
    public class Author : Base.Base
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime DateDeath { get; set; }
        public ICollection<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
    }
}
