using IoC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoC.Services
{
    public class MyService : IMyService
    {
        public string GetDate()
        {
            return System.DateTime.Now.ToLongDateString();
        }
    }
}
