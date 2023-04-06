using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCloudFireStore.Shared.Models
{
    [FirestoreData]
    public class Employee
    {
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }

        [FirestoreProperty]
        public string EmployeeName { get; set; }

        [FirestoreProperty]
        public string CityName { get; set; }

        [FirestoreProperty]
        public string Designation { get; set; }

        [FirestoreProperty]
        public string Gender { get; set; }
    }
}
