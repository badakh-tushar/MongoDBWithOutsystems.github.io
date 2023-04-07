using HotChocolate.Data;
using MongoDB.Driver;
using MongoDBPOCCsharp.Service;
using System;

namespace MongoDBPOCCsharp.Models
{
    public class Query
    {
        private EmployeeService _empService;

        private DepartmentService _deptService;

        private ProjectService _projectService;
        public Query(EmployeeService empService, DepartmentService deptService, ProjectService projectService)
        {
            _empService = empService;
            _deptService = deptService; 
            _projectService = projectService;
        }


        public async Task<ResponseEntity<EmployeeModel>> GetEmployeeAsync()
        {
            return await _empService.GetAsync();    
        }

        public async Task<ResponseEntity<DepartmentModel>> GetDepartmentAsync()
        {
            return await _deptService.GetAsync();
        }

        public async Task<ResponseEntity<EmployeeModel>> CreateEmployeeAsync(EmployeeModel employee)
        {
           
            var response = await _empService.CreateAsync(employee);
            return response;
        }

        public async Task<ResponseEntity<DepartmentModel>> CreateDepartmentAsync(DepartmentModel department)
        {

            var response = await _deptService.CreateAsync(department);
            return response;
        }

        public async Task<ResponseEntity<DepartmentModel>> UpdateDepartmentAsync(string id, DepartmentModel dept)
        {

            var response = await _deptService.UpdateAsync(id, dept);
            return response;
        }

        /*
        public IExecutable<Projects> GetPersons([Service] IMongoCollection<Projects> collection)
        {
            return collection.AsExecutable();
        }

        */
        
                public async Task<ResponseEntity<Projects>> CreatenewProjectAsync(Projects project)
                {

                    var response = await _projectService.CreateAsync(project);
                    return response;
                }

          
        /*
                public async Task<ResponseEntity<Projects>> GetProjectAsync()
                {

                    var response = await _projectService.GetAsync();
                    return response;
                }
        */
    }
}
