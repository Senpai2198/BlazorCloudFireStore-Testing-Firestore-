using BlazorCloudFireStore.Server.DataAccess;
using BlazorCloudFireStore.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCloudFireStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeDataAccessLayer objEmployee = new EmployeeDataAccessLayer();

        [HttpGet]
        public Task<List<Employee>> Get()
        {
            return objEmployee.GetAllEmployees();
        }
        
        [HttpGet("{id}")]
        public Task<Employee> Get(string id) 
        {
            return objEmployee.GetEmployeeData(id);
        }

        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            objEmployee.AddEmployee(employee);
        }

        [HttpPut]
        public void Put([FromBody] Employee employee) 
        {
            objEmployee.UpdateEmployee(employee);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            objEmployee.DeleteEmployee(id);
        }

        [HttpGet("GetCities")]
        public Task<List<Cities>> GetCities()
        {
            return objEmployee.GetCityData();
        }
    }
}
