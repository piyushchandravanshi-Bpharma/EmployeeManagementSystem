namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class AdminScheduleDto
    {
        public int AdminScheduleId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string AdminUserId { get; set; }
    }
}
