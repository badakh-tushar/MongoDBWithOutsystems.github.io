using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBPOCCsharp.Models
{
    public class EmployeeModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id
        {
            get;
            set;
        }
        public string? empName
        {
            get;
            set;
        }
        public string? empId
        {
            get;
            set;
        }

       
        public DepartmentModel? department { get; set; }
        public DateTime? JoiningDate
        {
            get;
            set;
        }
    }
}
