using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class EmployeeScheduleService : IEmployeeScheduleService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeScheduleDto>> GetAllSchedulesAsync()
        {
            return await _context.EmployeeSchedules
                                 .Select(schedule => new EmployeeScheduleDto
                                 {
                                     ScheduleId = schedule.ScheduleId,
                                     Title = schedule.Title,
                                     StartTime = schedule.StartTime,
                                     EndTime = schedule.EndTime,
                                     Location = schedule.Location,
                                     EmployeeId = schedule.EmployeeId
                                 }).ToListAsync();
        }

        public async Task<EmployeeScheduleDto> GetScheduleByIdAsync(int id)
        {
            var schedule = await _context.EmployeeSchedules.FindAsync(id);
            if (schedule == null) return null;

            return new EmployeeScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                Title = schedule.Title,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Location = schedule.Location,
                EmployeeId = schedule.EmployeeId
            };
        }

        public async Task<EmployeeScheduleDto> CreateScheduleAsync(EmployeeScheduleDto scheduleDto)
        {
            var schedule = new EmployeeSchedule
            {
                Title = scheduleDto.Title,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime,
                Location = scheduleDto.Location,
                EmployeeId = scheduleDto.EmployeeId
            };

            _context.EmployeeSchedules.Add(schedule);
            await _context.SaveChangesAsync();

            scheduleDto.ScheduleId = schedule.ScheduleId;
            return scheduleDto;
        }

        public async Task<EmployeeScheduleDto> UpdateScheduleAsync(int id, EmployeeScheduleDto scheduleDto)
        {
            var schedule = await _context.EmployeeSchedules.FindAsync(id);
            if (schedule == null) return null;

            schedule.Title = scheduleDto.Title;
            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            schedule.Location = scheduleDto.Location;
            schedule.EmployeeId = scheduleDto.EmployeeId;

            await _context.SaveChangesAsync();
            return scheduleDto;
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.EmployeeSchedules.FindAsync(id);
            if (schedule == null) return false;

            _context.EmployeeSchedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
