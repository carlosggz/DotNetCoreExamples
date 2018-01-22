using CustomAuthorization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthorization.Models
{
    public class HomeModel
    {
        public string User { get; set; }
        public string Email { get; set; }
        public MoviesChannels Channels { get; set; }
    }
}
