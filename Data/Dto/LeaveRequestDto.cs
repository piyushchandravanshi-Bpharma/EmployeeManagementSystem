namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class LeaveRequestDto
    {
        public int LeaveId { get; set; }
        public string UserId { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
