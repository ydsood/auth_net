using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB;
using Microsoft.Extensions.Configuration;

namespace auth_net.Models.Common
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
    }
}
