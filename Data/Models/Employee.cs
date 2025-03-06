using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string EmergencyContact { get; set; }
        public decimal Salary { get; set; }
        public string JobRole { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public bool TrainingRequired { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column("ApplicationUserId")]
        
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // Added EmployeeTasks
        public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
    }
}
