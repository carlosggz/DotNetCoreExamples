using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomConfiguration.Core
{
    public static class ApplicationValues
    {
        private static bool _initialized = false;

        public static string HelloValue { get; private set; }

        public static void Initialize(IConfiguration configuration)
        {
            if (_initialized)
                throw new Exception("Already initialized");

            _initialized = true;

            //Set the application settings
            HelloValue = configuration.GetValue<string>("Hello");
        }

    }
}
