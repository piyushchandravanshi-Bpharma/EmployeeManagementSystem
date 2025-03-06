using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }

        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // PENDING, APPROVED, REJECTED
    }
}
