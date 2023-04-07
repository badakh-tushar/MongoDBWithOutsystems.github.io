

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBPOCCsharp.Models
{

    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class DepartmentModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string name { get; set; }
    }
}
