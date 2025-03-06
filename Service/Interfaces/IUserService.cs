using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IUserService
    {
        Task<EmployeeDTO> CreateEmployeeAsync(RegisterEmployeeDTO employeeDTO);
    }

}
