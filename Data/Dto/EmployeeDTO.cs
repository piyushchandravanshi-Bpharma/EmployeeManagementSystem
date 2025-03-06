using EmployeeManagementSystem.Server.Data.Models;

namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class EmployeeDTO
    {
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
        public int DepartmentId { get; set; }
        public bool TrainingRequired { get; set; }
        public string UserId { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }

    }
}
