using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB;
using Microsoft.Extensions.Configuration;
using System.Web.Helpers;

namespace auth_net.DAO.Common
{
    public class MongoHelper<T> where T : class
    {
        public IMongoCollection<T> Collection { get; private set; }
        public MongoHelper()
        {
            // TODO :  Inject IOptions or IConfiguration
            var client = new MongoClient("mongodb://localhost:27017/auth_net");
            var db = client.GetDatabase("auth_net");
            Collection = db.GetCollection<T>(typeof(T).ToString());
        }

        public static string SaltAndEncryptPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool PasswordsMatch(string hashedPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }
    }
}
