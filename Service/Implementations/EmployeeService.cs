using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
          

            return await _context.Employees
                .Select(e => new EmployeeDTO
             {
                 EmployeeId = e.EmployeeId,
                 FirstName = e.FirstName,
                 LastName = e.LastName,
                 Photo = e.Photo,
                 Email = e.Email,
                 Address = e.Address,
                 Contact = e.Contact,
                 EmergencyContact = e.EmergencyContact,
                 Salary = e.Salary,
                 JobRole = e.JobRole,
                 DepartmentId = e.DepartmentId,
                 TrainingRequired = e.TrainingRequired,
                 UserId = e.UserId
                 //ApplicationUser = e.ApplicationUser

             })
            .ToListAsync();
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Photo = employee.Photo,
                Email = employee.Email,
                Address = employee.Address,
                Contact = employee.Contact,
                EmergencyContact = employee.EmergencyContact,
                Salary = employee.Salary,
                JobRole = employee.JobRole,
                DepartmentId = employee.DepartmentId,
                TrainingRequired = employee.TrainingRequired,
                UserId = employee.UserId
                
            };
        }

        public async Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTO employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Photo = employeeDto.Photo,
                Email = employeeDto.Email,
                Address = employeeDto.Address,
                Contact = employeeDto.Contact,
                EmergencyContact = employeeDto.EmergencyContact,
                Salary = employeeDto.Salary,
                JobRole = employeeDto.JobRole,
                DepartmentId = employeeDto.DepartmentId,
                TrainingRequired = employeeDto.TrainingRequired,
                UserId = employeeDto.UserId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Photo = employee.Photo,
                Email = employee.Email,
                Address = employee.Address,
                Contact = employee.Contact,
                EmergencyContact = employee.EmergencyContact,
                Salary = employee.Salary,
                JobRole = employee.JobRole,
                DepartmentId = employee.DepartmentId,
                TrainingRequired = employee.TrainingRequired,
                UserId = employee.UserId
            };
        }

        public async Task<EmployeeDTO> UpdateEmployeeAsync(int id, EmployeeDTO employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Photo = employeeDto.Photo;
            employee.Email = employeeDto.Email;
            employee.Address = employeeDto.Address;
            employee.Contact = employeeDto.Contact;
            employee.EmergencyContact = employeeDto.EmergencyContact;
            employee.Salary = employeeDto.Salary;
            employee.JobRole = employeeDto.JobRole;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.TrainingRequired = employeeDto.TrainingRequired;
            employee.UserId = employeeDto.UserId;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Photo = employee.Photo,
                Email = employee.Email,
                Address = employee.Address,
                Contact = employee.Contact,
                EmergencyContact = employee.EmergencyContact,
                Salary = employee.Salary,
                JobRole = employee.JobRole,
                DepartmentId = employee.DepartmentId,
                TrainingRequired = employee.TrainingRequired,
                UserId = employee.UserId
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetEmployeeIdByUserId(string userId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == userId);
            return employee?.EmployeeId ?? 0;
        }

        public async Task<string> GetEmployeFullNameByUserId(string userId)
        {
            var fullName = await _context.Employees
         .Where(e => e.UserId == userId)
         .Select(e => e.FirstName + " " + e.LastName)
         .FirstOrDefaultAsync();

            return fullName ?? "Admin";
                
        }

      
    }
}
