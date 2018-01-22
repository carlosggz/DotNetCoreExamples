using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomAuthorization.Core;
using CustomAuthorization.Infrastructure;
using CustomAuthorization.Infrastructure.Web;
using CustomAuthorization.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorization.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (IsLogged)
                return BackToHome();

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<AuthenticationModel> Login(LoginModel model)
        {
            if (IsLogged)
                return new AuthenticationModel() { Success = false, Messages = new List<string> { "User already logged" }};

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "test"),
                    new Claim(ClaimTypes.Email, "test@home.com")
                };

            if (model.Channels.HasFlag(MoviesChannels.Syfy))
                claims.Add(new Claim(Constants.MoviesClaim, MoviesChannels.Syfy.ToString()));

            if (model.Channels.HasFlag(MoviesChannels.Tnt))
                claims.Add(new Claim(Constants.MoviesClaim, MoviesChannels.Tnt.ToString()));

            var claimsIdentity = new ClaimsIdentity(claims, Constants.CookieName, ClaimTypes.Name, Constants.MoviesClaim);            

            await HttpContext.SignInAsync(
                Constants.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });

            return new AuthenticationModel() { Success = true, Url = Url.Action("Index", "Home") };
        }

        [Authorize]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext
                .SignOutAsync(Constants.AuthenticationScheme);

            return BackToHome();
        }
    }
}