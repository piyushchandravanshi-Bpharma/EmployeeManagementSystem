using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public decimal BaseSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary => BaseSalary - Deductions;
        public DateTime PayDate { get; set; }
    }
}
