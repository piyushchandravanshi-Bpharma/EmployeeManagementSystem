using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using Microsoft.AspNetCore.Identity;
using EmployeeManagementSystem.Server.Service.Interfaces;
using EmployeeManagementSystem.Server.Helper;
using EmployeeManagementSystem.Server.Constants;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<EmployeeDTO> CreateEmployeeAsync(RegisterEmployeeDTO employeeDTO)
        {


            var user = new ApplicationUser
            {
                UserName = employeeDTO.Email,
                Email = employeeDTO.Email,

                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, "G6m*k9Ws");
            Console.WriteLine("result set from UserService.class" + result);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user because: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            var roleToAssign = employeeDTO.Role.ToLower() switch
            {
                "admin" => RoleConstants.Admin,
                _ => RoleConstants.Employee
            };

            var resultRole = await _userManager.AddToRoleAsync(user, roleToAssign);
            if (!resultRole.Succeeded)
            {
                throw new Exception($"Failed to add role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var employee = new Employee
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Email = employeeDTO.Email,
                Photo = "NA",
                Address = employeeDTO.Address,
                Contact = employeeDTO.Contact,
                EmergencyContact = employeeDTO.Contact,
                Salary = employeeDTO.Salary,
                JobRole = employeeDTO.JobRole,
                DepartmentId = employeeDTO.DepartmentId,
                TrainingRequired = employeeDTO.TrainingRequired,
                UserId = user.Id

            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            //var emailSubject = "Welcome to EMS Portal - Your Account Details";
            //var emailBody = $"Hello {employeeDTO.FirstName},\n\nYour account has been created successfully!\n\n" +
            //                $"Username: {employeeDTO.Email}\n" +
            //                $"Temporary Password: {randomPassword}\n\n" +
            //                "Please login to the EMS portal and update your password.\n\nThank you!";

            //await _emailService.SendEmailAsync(employeeDTO.Email, emailSubject, emailBody);

            var employeeDTOResult = new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Photo = employee.Photo,
                Address = employee.Address,
                Contact = employee.Contact,
                EmergencyContact = employee.EmergencyContact,
                Salary = employee.Salary,
                JobRole = employee.JobRole,
                DepartmentId = employee.DepartmentId,
                UserId = employee.UserId,
                TrainingRequired = employee.TrainingRequired
            };

            return employeeDTOResult;
        }
    }
}
