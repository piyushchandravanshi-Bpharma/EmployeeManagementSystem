using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceDto> CreateAttendanceAsync(AttendanceDto attendanceDto)
        {
            var attendance = ConvertToModel(attendanceDto);
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
            return ConvertToDto(attendance);
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceByEmployeeIdAsync(int employeeId)
        {
            var attendanceRecords = await _context.Attendances
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();
            return attendanceRecords.Select(a => ConvertToDto(a));
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceByDayAsync(DateTime day)
        {
            var attendanceRecords = await _context.Attendances
                .Where(a => a.CheckIn.Date == day.Date)
                .ToListAsync();
            return attendanceRecords.Select(a => ConvertToDto(a));
        }

        public async Task<AttendanceDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            return attendance == null ? null : ConvertToDto(attendance);
        }



        public async Task<AttendanceDto> UpdateCheckOutStatusAsync(int id, DateTime checkOut)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                throw new ArgumentException("Attendance not found");
            }

            attendance.CheckOut = checkOut;
            attendance.Status = "CHECKED OUT";
            await _context.SaveChangesAsync();
            return ConvertToDto(attendance);
        }


        private AttendanceDto ConvertToDto(Attendance attendance)
        {
            return new AttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                CheckIn = attendance.CheckIn,
                CheckOut = attendance.CheckOut,
                Status = attendance.Status
            };
        }

        private Attendance ConvertToModel(AttendanceDto attendanceDto)
        {
            return new Attendance
            {
                AttendanceId = attendanceDto.AttendanceId,
                EmployeeId = attendanceDto.EmployeeId,
                CheckIn = attendanceDto.CheckIn,
                CheckOut = attendanceDto.CheckOut,
                Status = attendanceDto.Status
            };
        }
    }

}
