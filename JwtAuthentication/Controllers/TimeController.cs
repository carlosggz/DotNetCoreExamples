using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]    
    public class TimeController : Controller
    {
        [Authorize]
        [HttpGet]
        public string Get()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}
