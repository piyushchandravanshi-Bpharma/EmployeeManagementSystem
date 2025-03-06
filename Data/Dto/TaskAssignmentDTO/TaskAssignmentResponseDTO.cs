namespace EmployeeManagementSystem.Server.Data.Dto.TaskAssignmentDTO
{
    public class TaskAssignmentResponseDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public string Status { get; set; }
    }
}
