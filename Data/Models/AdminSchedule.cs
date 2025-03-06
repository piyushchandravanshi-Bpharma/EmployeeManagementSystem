using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class AdminSchedule
    {
        [Key]
        public int AdminScheduleId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }

        // Admin schedule is linked to an admin user
        [ForeignKey("ApplicationUser")]
        public string AdminUserId { get; set; }
        public ApplicationUser AdminUser { get; set; }
    }
}
