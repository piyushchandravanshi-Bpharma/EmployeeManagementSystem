using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTO employeeDto);
        Task<EmployeeDTO> UpdateEmployeeAsync(int id, EmployeeDTO employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<int> GetEmployeeIdByUserId(string userId);
        Task<String> GetEmployeFullNameByUserId(string userId);

       

    }
}
