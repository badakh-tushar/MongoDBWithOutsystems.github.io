using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBPOCCsharp.Models;

namespace MongoDBPOCCsharp.Service
{
    public class ProjectService
    {
        private readonly IMongoCollection<Projects> _projectsCollection;

        private readonly IMongoCollection<EmployeeModel> _employeesCollection;

        private ResponseEntity<Projects> _response;

        public ProjectService(
            IOptions<ProjectManagementStoreSetting> ProjectStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ProjectStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ProjectStoreDatabaseSettings.Value.DatabaseName);

            _projectsCollection = mongoDatabase.GetCollection<Projects>(
                    ProjectStoreDatabaseSettings.Value.ProjectCollection);


            _employeesCollection = mongoDatabase.GetCollection<EmployeeModel>(
                    ProjectStoreDatabaseSettings.Value.EmployeeCollection);


            _response = new ResponseEntity<Projects>();

        }

        public async Task<ResponseEntity<Projects>> GetAsync()
        {
            var projectsList = await _projectsCollection.Find(_ => true).ToListAsync();


            if (projectsList.Count == 0)
            {
                _response.DataList = null;
                _response.IsSuccess = false;
                _response.Message = "error";
            }
            else
            {
                _response.DataList = projectsList;
                _response.IsSuccess = true;
                _response.Message = "Success";
            }


            return _response;

        }

/*
        public async Task<ResponseEntity<DepartmentModel>> GetAsync(string id)
        {
            var department = await _departmentCollection.Find(x => x._id == id).FirstOrDefaultAsync();
            if (department != null)
            {
                _response.Data = department;
                _response.IsSuccess = true;
                _response.Message = "error";
            }
            else
            {
                _response.Data = null;
                _response.IsSuccess = true;
                _response.Message = "error";
            }
            return _response;

        }
*/
        public async Task<ResponseEntity<Projects>> CreateAsync(Projects project)
        {

            var empList = await _employeesCollection.Find(_ => true).ToListAsync();

            try
            {

              //  project.employees = new List<MongoDBRef>();
                foreach (var employee in empList)
                {
                   // project.employees.Add(new MongoDBRef("Employee", employee._id));
                }


                await _projectsCollection.InsertOneAsync(project);
                _response.IsSuccess = true;
                _response.Message = "success";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "error";
            }

            return _response;

        }
/*
        public async Task<ResponseEntity<DepartmentModel>> UpdateAsync(string id, DepartmentModel updatedDepartment)
        {

            if (id != null && updatedDepartment != null)
            {
                var filteredEmployeeList = ((await _employeesCollection.FindAsync(x => x.department._id == id)).ToListAsync()).Result;


                var UpdatedData = await _departmentCollection.ReplaceOneAsync(x => x._id.Equals(id), updatedDepartment, new UpdateOptions { IsUpsert = true });
                if (UpdatedData != null && filteredEmployeeList.Count > 0)
                {

                    foreach (EmployeeModel emp in filteredEmployeeList)
                    {

                        emp.department = updatedDepartment;

                        // await  _employeesCollection.ReplaceOneAsync(x=>x._id.Equals(id), emp, new UpdateOptions { IsUpsert = true });
                    }
                    _response.Data = updatedDepartment;
                    _response.IsSuccess = true;
                    _response.Message = "success";
                }


            }
            else
            {
                _response.Data = null;
                _response.IsSuccess = true;
                _response.Message = "error";

            }


            return _response;
        }

        public async Task<ResponseEntity<DepartmentModel>> RemoveAsync(string id)
        {
            var result = await _departmentCollection.DeleteOneAsync(x => x._id == id);
            if (result != null)
            {
                _response.IsSuccess = true;
                _response.Message = "success";
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "error";
            }
            return _response;
        }

        */

    }
}
