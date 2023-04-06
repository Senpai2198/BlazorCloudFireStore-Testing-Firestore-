using BlazorCloudFireStore.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorCloudFireStore.Client.Pages
{
    public class EmployeeDataModel : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }
        protected List<Employee> empList = new List<Employee>();
        protected Employee emp = new Employee();
        protected string SearchString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetEmployeeList();
        }
        protected async Task GetEmployeeList()
        {
            empList = await Http.GetFromJsonAsync<List<Employee>>("api/Employee");
        }
        protected async Task SearchEmployee()
        {
            await GetEmployeeList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                empList = empList.Where(x => x.EmployeeName.IndexOf(SearchString,StringComparison.OrdinalIgnoreCase)!=-1).ToList();
            }
        }
    }
}
