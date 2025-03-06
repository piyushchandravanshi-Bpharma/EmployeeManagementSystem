using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequestDto dto);

        Task<List<LeaveRequest>> GetAllLeaveRequestsAsync();

        Task<List<LeaveRequest>> GetUserLeaveRequestsAsync(string userId);

        Task<bool> UpdateLeaveRequestStatusAsync(int leaveId, string status);


    }
}
