﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthorization.Models
{
    public class AuthenticationModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> Messages { get; set; }
        public string Url { get; set; }
    }
}
