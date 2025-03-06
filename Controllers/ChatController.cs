using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("rooms")]
        public async Task<ActionResult<ChatRoomDto>> CreateChatRoom([FromBody] string name)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var chatRoom = await _chatService.CreateChatRoomAsync(name, userId);
            return Ok(chatRoom);
        }

        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<ChatRoomDto>>> GetChatRooms()
        {
            var rooms = await _chatService.GetAllChatRoomsAsync();
            return Ok(rooms);
        }

        [HttpPost("rooms/{roomId}/messages")]
        public async Task<ActionResult<ChatMessageDto>> SendMessage(int roomId, [FromBody] string content)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var message = await _chatService.SendMessageAsync(roomId, userId, content);
            return Ok(message);
        }

        [HttpGet("rooms/{roomId}/messages")]
        public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetRoomMessages(int roomId)
        {
            var messages = await _chatService.GetChatRoomMessagesAsync(roomId);
            return Ok(messages);
        }
    }
}
