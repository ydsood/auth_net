using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Linq;

using auth_net.Model;
using auth_net.DAO.Common;

namespace auth_net.DAO
{
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> _users;

        public static UserRepository Get()
        {
            return new UserRepository();
        }
        private UserRepository(string connection = "")
        {
            var mongoHelper = new MongoHelper<User>();
            _users = mongoHelper.Collection;
        }

        public User AddUser(User u)
        {
            u.Password = MongoHelper<User>.SaltAndEncryptPassword(u.Password);
            _users.InsertOne(u);
            try
            {
                return GetUser(u.UserName);
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            var filter = Builders<User>.Filter.Empty;
            return _users.Find<User>(filter).ToList();
        }

        public User GetUser(string userName)
        {
            var filter = Builders<User>.Filter.Eq("UserName", userName);
            var result = _users.Find(filter);
            if (!result.Any<User>())
                throw new KeyNotFoundException($"UserName {userName} does not exist in the system");
            return result.First<User>();
        }

        public bool RemoveUser(string userName)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserName, userName);
            var result = _users.DeleteOne(filter);
            return result.IsAcknowledged;
        }

        public bool UpdateUser(string userName, User user)
        {
            var update = Builders<User>.Update
                        .Set(u => u.UserRole, user.UserRole)
                        .Set(u => u.FullName, user.FullName)
                        .Set(u => u.Password, MongoHelper<User>.SaltAndEncryptPassword(user.Password));
            var result = _users.UpdateOne<User>(u => u.UserName == userName, update);
            return result.IsAcknowledged;
        }
    }
}
