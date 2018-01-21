using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuthentication.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Invalid Login")]
        [StringLength(50, ErrorMessage = "Max length for login is 50 characters")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Invalid password")]
        [StringLength(50, ErrorMessage = "Max length for login is 50 characters")]
        public string Password { get; set; }
    }
}
