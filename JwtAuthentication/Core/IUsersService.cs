using JwtAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Core
{
    public interface IUsersService
    {
        UserEntity GetUserByAuth(string login, string password);
    }
}
