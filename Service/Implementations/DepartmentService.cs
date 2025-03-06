using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return null;

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return null;

            department.DepartmentName = departmentDto.DepartmentName;
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
