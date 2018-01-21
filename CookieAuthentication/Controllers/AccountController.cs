using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CookieAuthentication.Core;
using CookieAuthentication.Infrastructure;
using CookieAuthentication.Infrastructure.Web;
using CookieAuthentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthentication.Controllers
{
    public class AccountController : BaseController
    {
        IUsersManagerService _usersManager = null;

        public AccountController(IUsersManagerService usersManager)
        {
            _usersManager = usersManager;
        }

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

            if (!ModelState.IsValid)
                return new AuthenticationModel() { Success = false, Messages = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage) };

            var user = _usersManager.GetUserByAuth(model.Login, model.Password);

            if (user == null)
                return new AuthenticationModel() { Success = false, Messages = new List<string> { "Invalid user name or password" } };

            var claimsIdentity = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(Constants.FullNameClaim, user.FullName)
                },
                Constants.CookieName
            );

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