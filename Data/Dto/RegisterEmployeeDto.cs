using EmployeeManagementSystem.Server.Data.Models;

namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class RegisterEmployeeDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }       
        public decimal Salary { get; set; }
        public string JobRole { get; set; }
        public int DepartmentId { get; set; }
        public bool TrainingRequired { get; set; }

        public string Role { get; set; }
       
    }
}
