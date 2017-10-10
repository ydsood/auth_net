using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace auth_net.Model
{
    public class User : BaseDocument
    {
        // TODO : Add Password field with encryption/hashing
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement]
        public string Password { get; set; }
        [BsonElement("FullName")]
        public string FullName { get; set; }
        [BsonElement("UserRole")]
        public string UserRole { get; set; }
    }
}
