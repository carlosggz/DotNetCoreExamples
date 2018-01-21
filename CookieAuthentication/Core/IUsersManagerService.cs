using CookieAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuthentication.Core
{
    public interface IUsersManagerService
    {
        UserEntity GetUserByAuth(string login, string password);
    }
}
