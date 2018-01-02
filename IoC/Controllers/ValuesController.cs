using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoC.Core;
using Microsoft.AspNetCore.Mvc;

namespace IoC.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMyService _service = null;

        public ValuesController(IMyService service)
        {
            _service = service;
        }

        [HttpGet]
        public string Get()
        {
            return _service.GetDate();
        }
    }
}
