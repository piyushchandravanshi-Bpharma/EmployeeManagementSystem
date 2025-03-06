namespace EmployeeManagementSystem.Server.Data.Dto.TaskAssignmentDTO
{
    public class TaskAssignmentRequestDTO
    {
        public int EmployeeId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public IFormFile? InputFile { get; set; }
    }
}
