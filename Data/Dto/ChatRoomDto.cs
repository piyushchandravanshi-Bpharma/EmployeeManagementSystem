namespace EmployeeManagementSystem.Server.Data.Dto
{
    public class ChatRoomDto
    {
        public int ChatRoomId { get; set; }
        public string ChatRoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
