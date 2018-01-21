using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthentication.Core;
using JwtAuthentication.Entities;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Controllers
{
    [Route("api/login")]
    public class AccountController : Controller
    {
        private IConfiguration _config;
        IUsersService _usersService = null;

        public AccountController(IConfiguration config, IUsersService usersService)
        {
            _config = config;
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public IActionResult Token(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage) });

            var user = _usersService.GetUserByAuth(model.Login, model.Password);

            if (user == null)
                return Unauthorized();

            return Ok(new { token = GenerateToken(user) });
        }

        private string GenerateToken(UserEntity user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); // $"{_config["Jwt:Key"] }.{user.UserName}"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
