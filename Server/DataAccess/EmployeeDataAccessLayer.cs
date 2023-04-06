using BlazorCloudFireStore.Shared.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.Threading;

namespace BlazorCloudFireStore.Server.DataAccess
{
    public class EmployeeDataAccessLayer
    {
        string projectId;
        FirestoreDb firestoreDb;
        public EmployeeDataAccessLayer()
        {
            string filePath = "C:\\FireStoreAPIKey\\coffee-form-cloud-db-firebase-adminsdk-ejmcg-c2bda31ae3.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            projectId = "coffee-form-cloud-db";
            firestoreDb = FirestoreDb.Create(projectId);
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                Query employeeQuery = firestoreDb.Collection("employees");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                List<Employee> FirstEmployee = new List<Employee>();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        Employee newUser = JsonConvert.DeserializeObject<Employee>(json);
                        newUser.EmployeeId = documentSnapshot.Id;
                        newUser.Date = documentSnapshot.CreateTime.Value.ToDateTime();
                        FirstEmployee.Add(newUser);
                    }
                }
                List<Employee> storedEmployeeList = FirstEmployee.OrderBy(x => x.Date).ToList();
                return storedEmployeeList;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async void AddEmployee(Employee employee)
        {
            try
            {
                CollectionReference colRef = firestoreDb.Collection("employees");
                await colRef.AddAsync(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void UpdateEmployee(Employee employee)
        {
            try
            {
                DocumentReference empRef = firestoreDb.Collection("employees").Document(employee.EmployeeId);
                await empRef.SetAsync(employee, SetOptions.Overwrite);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Employee> GetEmployeeData(string id)
        {
            try
            {
                DocumentReference docRef = firestoreDb.Collection("employees").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    Employee emp = snapshot.ConvertTo<Employee>();
                    emp.EmployeeId = snapshot.Id;
                    return emp;
                }
                else
                {
                    return new Employee();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void DeleteEmployee(string id)
        {
            try
            {
                DocumentReference empRef = firestoreDb.Collection("employees").Document(id);
                await empRef.DeleteAsync();
            }
            catch(Exception) 
            {
                throw;
            }
        }
        public async Task<List<Cities>> GetCityData()
        {
            try
            {
                Query citiesQuery = firestoreDb.Collection("cities");
                QuerySnapshot citiesQuerySnapshot = await citiesQuery.GetSnapshotAsync();
                List<Cities> firstCity = new List<Cities>();
                foreach (DocumentSnapshot documentSnapshot in citiesQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        Cities newCity = JsonConvert.DeserializeObject<Cities>(json);
                        firstCity.Add(newCity);
                    }
                }
                return firstCity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
