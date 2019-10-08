using App.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace App.DataAccessLayer.Entities
{
    public class Author : Essence
    {
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime DateDeath { get; set; }
        public ICollection<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }


    }
}
