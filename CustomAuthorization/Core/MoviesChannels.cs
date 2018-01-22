using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthorization.Core
{
    [Flags]
    public enum MoviesChannels: int
    {
        None,
        Syfy,
        Tnt
    }
}
