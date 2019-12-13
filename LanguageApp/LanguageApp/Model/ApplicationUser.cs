using System;
using Microsoft.AspNetCore.Identity;

namespace LanguageApp.Model
{
    public class ApplicationUser: IdentityUser
    {
        public int IdTable { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DTLogin { get; set; }
    }
}
