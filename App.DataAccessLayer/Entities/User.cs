using Microsoft.AspNetCore.Identity;

namespace App.DataAccessLayer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User(string userName)
        {
            UserName = userName;
        }
        public User()
        {

        }

    }
}
