using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationManagement.API
{
    public class AdminUser : IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }


    }
}