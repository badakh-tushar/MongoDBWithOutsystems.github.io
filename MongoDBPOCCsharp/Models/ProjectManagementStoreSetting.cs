namespace MongoDBPOCCsharp.Models
{
    public class ProjectManagementStoreSetting
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;


        public string EmployeeCollection { get; set; } = null!;

        public string ProjectCollection { get; set; } = null!;

        public string DepartmentCollection { get; set; } = null!;
    }
}
