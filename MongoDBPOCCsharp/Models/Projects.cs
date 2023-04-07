using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBPOCCsharp.Models
{
    public class Projects
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string? projId { get; set; }
        public string? projName { get; set; }


        public List<EmployeeModel> Employees { get; set; }


       // public List<MongoDBRef> employees { get; set; }


    }
}
