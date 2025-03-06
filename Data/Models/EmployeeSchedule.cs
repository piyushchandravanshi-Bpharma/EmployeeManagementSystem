using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class EmployeeSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }

        // Schedule is linked to a specific Employee
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
