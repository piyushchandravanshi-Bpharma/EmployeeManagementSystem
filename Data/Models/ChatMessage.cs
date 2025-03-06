using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key for ChatRoom
        [Required]
        public int ChatRoomId { get; set; }

        [ForeignKey("ChatRoomId")]
        public ChatRoom ChatRoom { get; set; } = null!; 

        // Keep SenderId without ApplicationUser navigation
        [Required]
        [MaxLength(450)]
        public string SenderId { get; set; } = string.Empty;
    }
}
