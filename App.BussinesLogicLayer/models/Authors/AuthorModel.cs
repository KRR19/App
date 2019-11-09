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
        public List<Guid> AuthorId { get; set; }
        public List<string> AuthorName { get; set; }
    }
}
