using App.DataAccessLayer.Entities.Base;

namespace App.DataAccessLayer.Entities
{
    public class User : Essence
    {
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }

    }
}
