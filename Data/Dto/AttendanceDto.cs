namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Status { get; set; }
    }
}
