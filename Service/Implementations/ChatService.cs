using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChatRoomDto> CreateChatRoomAsync(string name, string userId)
        {
            var chatRoom = new ChatRoom
            {
                ChatRoomName = name,
                CreatedBy = userId
            };

            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return new ChatRoomDto
            {
                ChatRoomId = chatRoom.ChatRoomId,
                ChatRoomName = chatRoom.ChatRoomName,
                CreatedAt = chatRoom.CreatedAt,
                CreatedBy = chatRoom.CreatedBy
            };
        }

        public async Task<ChatMessageDto> SendMessageAsync(int chatRoomId, string userId, string content)
        {
            var message = new ChatMessage
            {
                ChatRoomId = chatRoomId,
                SenderId = userId,
                Content = content
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return new ChatMessageDto
            {
                Id = message.Id,
                Content = message.Content,
                ChatRoomId = message.ChatRoomId,
                SenderId = message.SenderId
            };
        }

        public async Task<IEnumerable<ChatRoomDto>> GetAllChatRoomsAsync()
        {
            return await _context.ChatRooms
                .Select(r => new ChatRoomDto
                {
                    ChatRoomId = r.ChatRoomId,
                    ChatRoomName = r.ChatRoomName,
                    CreatedAt = r.CreatedAt,
                    CreatedBy = r.CreatedBy
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatMessageDto>> GetChatRoomMessagesAsync(int chatRoomId)
        {
            return await _context.ChatMessages
                .Where(m => m.ChatRoomId == chatRoomId)
                .Select(m => new ChatMessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    ChatRoomId = m.ChatRoomId,
                    SenderId = m.SenderId,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();
        }
    }
}
