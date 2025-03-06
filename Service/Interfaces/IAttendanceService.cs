using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> CreateAttendanceAsync(AttendanceDto attendanceDto);
        Task<IEnumerable<AttendanceDto>> GetAttendanceByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<AttendanceDto>> GetAttendanceByDayAsync(DateTime day);
        Task<AttendanceDto> GetAttendanceByIdAsync(int id);
        Task<AttendanceDto> UpdateCheckOutStatusAsync(int id, DateTime checkOut);

    }

}
