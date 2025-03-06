using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IChatService
    {
        Task<ChatRoomDto> CreateChatRoomAsync(string name, string userId);
        Task<ChatMessageDto> SendMessageAsync(int chatRoomId, string userId, string content);
        Task<IEnumerable<ChatRoomDto>> GetAllChatRoomsAsync();
        Task<IEnumerable<ChatMessageDto>> GetChatRoomMessagesAsync(int chatRoomId);
    }
}
