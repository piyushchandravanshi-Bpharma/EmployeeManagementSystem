using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementSystem.Server.Constants; // Add this line

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class TaskAssignment
    {

        [Key]
        public int Id { get; set; }

        // Foreign key to Employees table
        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }  // Navigation property

        // Foreign key to ApplicationUser (for the user who assigned the task)
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }  // Navigation property

        [MaxLength(500)]
        public string Description { get; set; } // From TaskAssignments

        public DateTime Deadline { get; set; }

        public string Status { get; set; } = EmployeeTaskStatus.Pending.ToString();

        public string? InputFilePath { get; set; }

        public string? OutputFilePath { get; set; }
    }
}
