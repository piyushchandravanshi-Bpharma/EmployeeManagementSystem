namespace EmployeeManagementSystem.Server.Data.Dto.TaskAssignmentDTO
{
    public class TaskUpdateStatusDTO
    {
        public int TaskId { get; set; }
        public string Status { get; set; }
        public IFormFile? OutputFile { get; set; }
    }
}
