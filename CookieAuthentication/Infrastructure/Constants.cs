using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuthentication.Infrastructure
{
    public static class Constants
    {
        public const string CookieName = "MyCookieData";
        public const string AuthenticationScheme = "CookieAuthentication";
        public const string FullNameClaim = "FullName";
        public const string XrsfToken = "X-CSRF-TOKEN";
    }
}
