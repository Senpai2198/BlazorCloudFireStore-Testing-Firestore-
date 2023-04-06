using BlazorCloudFireStore.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorCloudFireStore.Client.Pages
{
    public class DeleteEmployeeModel : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        public NavigationManager UrlNavigationManager { get; set; }
        [Parameter]
        public string empID { get; set; }
        protected string Title = "Delete";
        public Employee emp = new Employee();
        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(empID))
            {
                Title = "Delete";
                emp = await Http.GetFromJsonAsync<Employee>("api/Employee/" + empID);
            }
        }
        protected async Task DeleteEmployee()
        {
            await Http.DeleteAsync("api/Employee/" + empID);
            Cancel();
        }
        public void Cancel()
        {
            UrlNavigationManager.NavigateTo("/employeerecords");
        }
    }
}
