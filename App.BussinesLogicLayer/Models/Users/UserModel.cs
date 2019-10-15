using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace App.BussinesLogicLayer.Models.Users
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
