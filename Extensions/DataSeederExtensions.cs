using EmployeeManagementSystem.Server.Constants;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Extensions
{
    public static class DataSeederExtensions
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Ensure database is created
                await _context.Database.EnsureCreatedAsync();

                // Seed Roles
                foreach (var role in RoleConstants.AllRoles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Seed Department
                string departmentName = "Administration";
                var existingDepartment = await _context.Departments
                    .FirstOrDefaultAsync(d => d.DepartmentName == departmentName);

                if (existingDepartment == null)
                {
                    existingDepartment = new Department { DepartmentName = departmentName };
                    _context.Departments.Add(existingDepartment);
                    await _context.SaveChangesAsync();
                }

                // Seed Admin User
                string email = "admin@admin.com";
                string password = "Admin@1234";
                var existingUser = await userManager.FindByEmailAsync(email);

                if (existingUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, RoleConstants.Admin);

                        // Check if employee already exists
                        var existingEmployee = await _context.Employees
                            .FirstOrDefaultAsync(e => e.Email == email);

                        if (existingEmployee == null)
                        {
                            var employee = new Employee
                            {
                                FirstName = "first",
                                LastName = "admin",
                                Email = email,
                                Photo = "default.jpg",
                                Address = "Default Address",
                                Contact = "000-000-0000",
                                EmergencyContact = "000-000-0000",
                                Salary = 0.00m,
                                JobRole = "Administrator",
                                DepartmentId = existingDepartment.DepartmentId,
                                TrainingRequired = false,
                                UserId = user.Id  // This matches the property name in Employee model
                            };

                            _context.Employees.Add(employee);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new Exception($"Failed to create admin user: {errors}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error - in production, use proper logging
                Console.WriteLine($"Error seeding data: {ex.Message}");
                throw; // Re-throw to ensure the error is not silently swallowed
            }
        }
    }
}
