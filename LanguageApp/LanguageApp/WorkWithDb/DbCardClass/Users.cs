using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.WorkWithDb.DbCardClass
{
    public class Users
    {
        [Required]
        public Int64 IdTable { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password), StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6), Display(Name = "Password")]
        public string Password { get; set; }
        public DateTime DateLogin { get; set; }
    }
}
