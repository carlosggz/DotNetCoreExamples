using JwtAuthentication.Core;
using JwtAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Services
{
    public class UsersService : IUsersService
    {
        private List<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity(){ UserName = "test", Password = "test", FullName = "Test User", Email = "test@test.com" }
        };

        public UsersService()
        { }

        public UserEntity GetUserByAuth(string login, string password)
        {
            return string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)
                ? null
                : _users.SingleOrDefault(x => x.UserName == login && x.Password == password);
        }
    }
}
