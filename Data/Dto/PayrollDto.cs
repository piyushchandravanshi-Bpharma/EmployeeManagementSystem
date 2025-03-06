namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class PayrollDto
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayDate { get; set; }
    }
}
