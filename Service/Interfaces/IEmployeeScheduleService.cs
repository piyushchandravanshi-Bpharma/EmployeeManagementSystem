using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IEmployeeScheduleService
    {
        Task<List<EmployeeScheduleDto>> GetAllSchedulesAsync();
        Task<EmployeeScheduleDto> GetScheduleByIdAsync(int id);
        Task<EmployeeScheduleDto> CreateScheduleAsync(EmployeeScheduleDto schedule);
        Task<EmployeeScheduleDto> UpdateScheduleAsync(int id, EmployeeScheduleDto schedule);
        Task<bool> DeleteScheduleAsync(int id);
    }
}
