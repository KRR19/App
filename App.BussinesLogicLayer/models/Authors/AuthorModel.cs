using System;
using System.Collections.Generic;

namespace App.BussinesLogicLayer.models.Authors
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime DateDeath { get; set; }
    }
}
