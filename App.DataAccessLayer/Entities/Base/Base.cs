using System;

namespace App.DataAccessLayer.Entities.Base
{
    public class Base
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
