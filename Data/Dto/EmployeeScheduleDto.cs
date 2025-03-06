namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class EmployeeScheduleDto
    {
        public int ScheduleId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public int EmployeeId { get; set; }
    }
}
