using CustomAuthorization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthorization.Infrastructure
{
    public static class Constants
    {
        public const string CookieName = "MyCookieData";
        public const string AuthenticationScheme = "CustomAuthorization";
        public const string XrsfToken = "X-CSRF-TOKEN";
        public const string MoviesClaim = "MoviesChannels";

        public const string PolicySyfy = "PolicySyfy";
        public const string PolicyTnt = "PolicyTnt";

        public const string ClaimSyfy = "ClaimSyfy";
        public const string ClaimTnt = "ClaimTnt";
    }
}
