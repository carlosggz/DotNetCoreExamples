using CustomAuthorization.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomAuthorization.Infrastructure.Web
{
    public abstract class BaseController: Controller
    {
        public BaseController()
        {}

        protected bool IsLogged => User != null && User.Identity != null && User.Identity.IsAuthenticated;
        protected string LoggedUser => GetClaim(ClaimTypes.Name);
        protected string LoggedEmail => GetClaim(ClaimTypes.Email);


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

        protected MoviesChannels GetChannels()
        {
            if (!IsLogged)
                return MoviesChannels.None;

            var claim = (User.Identity as ClaimsIdentity)
                .Claims
                .Where(x => x.Type == Constants.MoviesClaim)
                .Select(x => (int)Enum.Parse(typeof(MoviesChannels), x.Value))
                .Sum();

            return (MoviesChannels)claim;
        }
    }
}
