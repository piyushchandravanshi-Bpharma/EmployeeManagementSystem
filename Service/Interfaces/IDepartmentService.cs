using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
