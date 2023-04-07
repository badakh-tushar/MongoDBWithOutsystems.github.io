
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBPOCCsharp.Models;

namespace MongoDBPOCCsharp.Service
{
    public class EmployeeService
    {

        private readonly IMongoCollection<EmployeeModel> _employeesCollection;

        private ResponseEntity<EmployeeModel> _response;

        public EmployeeService(
            IOptions<ProjectManagementStoreSetting> EmployeeStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EmployeeStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EmployeeStoreDatabaseSettings.Value.DatabaseName);

            _employeesCollection = mongoDatabase.GetCollection<EmployeeModel>(
                    EmployeeStoreDatabaseSettings.Value.EmployeeCollection);

            _response = new ResponseEntity<EmployeeModel>();

        }

        public async Task<ResponseEntity<EmployeeModel>> GetAsync()
        {
            var employeeeList = await _employeesCollection.Find(_ => true).ToListAsync();


            if(employeeeList.Count == 0)
            {
                _response.DataList = employeeeList;
                _response.IsSuccess= false;
                _response.Message = "error";
            }
            else
            {
                _response.DataList = employeeeList;
                _response.IsSuccess = true;
                _response.Message = "Success";
            }
           

            return _response;   

        }


        public async Task<ResponseEntity<EmployeeModel>> GetAsync(string id){
         var employee =    await _employeesCollection.Find(x => x._id == id).FirstOrDefaultAsync();
           if(employee != null)
            {
                _response.Data = employee;
                _response.IsSuccess = true;
                _response.Message = "error";
            }
            else
            {
                _response.Data = null;
                _response.IsSuccess = true;
                _response.Message= "error";
            }
           return _response;

    } 

    public async Task<ResponseEntity<EmployeeModel>> CreateAsync(EmployeeModel employee) {
            try
            {
                await _employeesCollection.InsertOneAsync(employee);
                _response.IsSuccess = true;
                _response.Message = "success";

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "error";
            }
              
            return _response;

        }

    public async Task<ResponseEntity<EmployeeModel>> UpdateAsync(string id, EmployeeModel updatedEmployee) {

            if(id!=null&& updatedEmployee!=null)
            {
                var UpdatedData = await _employeesCollection.ReplaceOneAsync(x => x._id.Equals(id), updatedEmployee);

                _response.Data = updatedEmployee;
                _response.IsSuccess = true;
                _response.Message = "success";
            }
            else
            {
                _response.Data = null;  
                _response.IsSuccess = true;
                _response.Message = "error";

            }
       

            return _response;
        }

    public async Task<ResponseEntity<EmployeeModel>> RemoveAsync(string id) {
       var result =  await _employeesCollection.DeleteOneAsync(x => x._id == id);
            if(result != null)
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
