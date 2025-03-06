using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class Department
    {

        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }

        // Navigation Property
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
