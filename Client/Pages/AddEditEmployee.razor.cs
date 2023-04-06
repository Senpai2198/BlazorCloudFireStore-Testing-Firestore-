using BlazorCloudFireStore.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorCloudFireStore.Client.Pages
{
    public class AddEditEmployeeModel : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        public NavigationManager UrlNavigationManager { get; set; }
        [Parameter]
        public string empID { get; set; }
        protected string Title = "Add";
        public Employee emp = new Employee();
        protected List<Cities> cityList = new List<Cities>();
        protected override async Task OnInitializedAsync()
        {
            await GetCityList();
        }
        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(empID))
            {
                Title = "Edit";
                emp = await Http.GetFromJsonAsync<Employee>("api/employee/" + empID);
            }
        }
        protected async Task GetCityList()
        {
            cityList = await Http.GetFromJsonAsync<List<Cities>>("api/employee/GetCities");
        }
        protected async Task SaveEmployee()
        {
            if (emp.EmployeeId == null)
            {
                emp.EmployeeId = "dummytext";
                await Http.PostAsJsonAsync("api/employee/", emp);
            }
            else
            {
                await Http.PutAsJsonAsync("api/employee/", emp);
            }
            Cancel();
        }
        public void Cancel()
        {
            UrlNavigationManager.NavigateTo("/employeerecords");
        }
    }
}
