using System;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Domain.Entity
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string Organization { get; set; }
    }
}