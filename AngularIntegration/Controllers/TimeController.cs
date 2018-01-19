using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AngularIntegration.Controllers
{
    [Route("api/[controller]")]
    public class TimeController : Controller
    {
        [HttpGet]
        public object Get()
        {
            var time = DateTime.Now;

            return new
            { 
                hour = time.Hour,
                minute = time.Minute,
                second = time.Second,
                formatted = DateTime.Now.ToLongTimeString()
            };
        }
    }
}