using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookieAuthentication.Infrastructure.Web
{
    public abstract class BaseController: Controller
    {
        public BaseController()
        {}

        protected bool IsLogged => User != null && User.Identity != null && User.Identity.IsAuthenticated;
        protected string LoggedUser => GetClaim(ClaimTypes.Name);
        protected string LoggedEmail => GetClaim(ClaimTypes.Email);
        protected string LoggedName => GetClaim(Constants.FullNameClaim);

        protected IActionResult BackToHome()
        {
            return Redirect("~/");
        }

        private string GetClaim(string claimType)
        {
            return !IsLogged
                ? string.Empty
                : (User.Identity as ClaimsIdentity).Claims.Single(x => x.Type == claimType).Value;
        }
    }
}
