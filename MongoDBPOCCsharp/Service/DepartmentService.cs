using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBPOCCsharp.Models;

namespace MongoDBPOCCsharp.Service
{
    public class DepartmentService
    {
        private readonly IMongoCollection<DepartmentModel> _departmentCollection;

        private readonly IMongoCollection<EmployeeModel> _employeesCollection;

        private ResponseEntity<DepartmentModel> _response;

        public DepartmentService(
            IOptions<ProjectManagementStoreSetting> DepartmentStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DepartmentStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DepartmentStoreDatabaseSettings.Value.DatabaseName);

            _departmentCollection = mongoDatabase.GetCollection<DepartmentModel>(
                    DepartmentStoreDatabaseSettings.Value.DepartmentCollection);


            _employeesCollection = mongoDatabase.GetCollection<EmployeeModel>(
                    DepartmentStoreDatabaseSettings.Value.EmployeeCollection);


            _response = new ResponseEntity<DepartmentModel>();

        }


        public async Task<ResponseEntity<DepartmentModel>> GetAsync()
        {
            var deprtmentList = await _departmentCollection.Find(_ => true).ToListAsync();


            if (deprtmentList.Count == 0)
            {
                _response.DataList = deprtmentList;
                _response.IsSuccess = false;
                _response.Message = "error";
            }
            else
            {
                _response.DataList = deprtmentList;
                _response.IsSuccess = true;
                _response.Message = "Success";
            }


            return _response;

        }


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

        public async Task<ResponseEntity<DepartmentModel>> CreateAsync(DepartmentModel department)
        {
            try
            {
                await _departmentCollection.InsertOneAsync(department);
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

        public async Task<ResponseEntity<DepartmentModel>> UpdateAsync(string id, DepartmentModel updatedDepartment)
        {
           
            if (id != null && updatedDepartment != null)
            {
                var filteredEmployeeList = ((await _employeesCollection.FindAsync(x => x.department._id == id)).ToListAsync()).Result;
               
                 
                var UpdatedData = await _departmentCollection.ReplaceOneAsync(x => x._id.Equals(id), updatedDepartment, new UpdateOptions { IsUpsert = true });
                if(UpdatedData!= null && filteredEmployeeList.Count>0) 
                {
                    
                    foreach (EmployeeModel emp in filteredEmployeeList)
                    {
                        
                        emp.department= updatedDepartment; 
                        
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


    }
}
