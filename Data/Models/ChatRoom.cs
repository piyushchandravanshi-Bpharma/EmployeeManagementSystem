using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class ChatRoom
    {
        [Key]
        public int ChatRoomId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ChatRoomName { get; set; } = string.Empty; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // CreatedBy references ApplicationUser
        [ForeignKey("ApplicationUser")]
        public string CreatedBy { get; set; } = string.Empty;

        public ApplicationUser ApplicationUser { get; set; } = null!;

        // One-to-Many: One ChatRoom can have many messages
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
