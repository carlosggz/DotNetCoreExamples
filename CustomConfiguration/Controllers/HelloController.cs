using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomConfiguration.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CustomConfiguration.Controllers
{
    [Produces("application/json")]
    [Route("api/Hello")]
    public class HelloController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return ApplicationValues.HelloValue;
        }
    }
}