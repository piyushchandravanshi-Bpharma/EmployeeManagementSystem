using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Server.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>(); // Fix for CS8618
        //public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>(); // Fix for CS8618
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>(); // Fix for CS8618
    }
}
