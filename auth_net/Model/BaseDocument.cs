using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace auth_net.Model
{
    public class BaseDocument
    {
        [BsonId]
        public ObjectId _id { get; set; }
    }
}
