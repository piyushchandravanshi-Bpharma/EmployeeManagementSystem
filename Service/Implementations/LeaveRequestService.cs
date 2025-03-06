using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Server.Service.Interfaces;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class LeaveRequestService : ILeaveRequestService

    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Create Leave Request (Employee)
        public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequestDto dto)
        {
            var leaveRequest = new LeaveRequest
            {
                UserId = dto.UserId,
                LeaveType = dto.LeaveType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "PENDING"
            };

            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return leaveRequest;
        }

        // ✅ View All Leave Requests (Admin)
        public async Task<List<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _context.LeaveRequests.Include(l => l.ApplicationUser).ToListAsync();
        }

        // ✅ View Own Leave Requests (Employee)
        public async Task<List<LeaveRequest>> GetUserLeaveRequestsAsync(string userId)
        {
            return await _context.LeaveRequests
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        // ✅ Update Leave Request Status (Admin)
        public async Task<bool> UpdateLeaveRequestStatusAsync(int leaveId, string status)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveId);
            if (leaveRequest == null) return false;

            leaveRequest.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
