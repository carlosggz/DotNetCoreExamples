using CookieAuthentication.Core;
using CookieAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuthentication.Services
{
    public class UsersManagerService: IUsersManagerService
    {
        private List<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity(){ UserName = "test", Password = "test", FullName = "Test User", Email = "test@test.com" }
        };

        public UsersManagerService()
        {}

        public UserEntity GetUserByAuth(string login, string password)
        {
            return string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)
                ? null
                : _users.SingleOrDefault(x => x.UserName == login && x.Password == password);
        }
    }
}
