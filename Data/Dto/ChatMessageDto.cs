namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ChatRoomId { get; set; }
        public string SenderId { get; set; }
    }
}
