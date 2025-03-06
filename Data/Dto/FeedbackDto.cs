namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int EmployeeId { get; set; }
    }
}
