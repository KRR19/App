using System;

namespace App.DataAccessLayer.Entities.Base
{
    public class Essence
    {
        public int Id { get; set; }
        public DateTime CreationData { get; set; }
        public bool IsRemoved { get; set; }
    }
}
