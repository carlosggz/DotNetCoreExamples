using CustomAuthorization.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthorization.Models
{
    public class LoginModel
    {
        public MoviesChannels Channels { get; set; }
    }
}
