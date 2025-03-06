using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // CreatedBy references ApplicationUser
        [ForeignKey("ApplicationUser")]
        public string CreatedBy { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
