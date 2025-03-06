using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employees");
                return StatusCode(500, "An error occurred while fetching employees.");
            }
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return NotFound($"Employee with ID {id} not found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee details");
                return StatusCode(500, "An error occurred while fetching employee details.");
            }
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null || string.IsNullOrWhiteSpace(employeeDto.FirstName) || string.IsNullOrWhiteSpace(employeeDto.LastName))
                return BadRequest("Invalid employee data.");

            try
            {
                var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                return StatusCode(500, "An error occurred while creating the employee.");
            }
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null || string.IsNullOrWhiteSpace(employeeDto.FirstName) || string.IsNullOrWhiteSpace(employeeDto.LastName))
                return BadRequest("Invalid employee data.");

            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                if (updatedEmployee == null)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return NotFound($"Employee with ID {id} not found.");
                }
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee");
                return StatusCode(500, "An error occurred while updating the employee.");
            }
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool deleted = await _employeeService.DeleteEmployeeAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return NotFound($"Employee with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee");
                return StatusCode(500, "An error occurred while deleting the employee.");
            }
        }

        // GET: api/employee/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEmployeeIdByUserId(string userId)
        {
            try
            {
                var employeeId = await _employeeService.GetEmployeeIdByUserId(userId);
                if (employeeId == 0)
                {
                    _logger.LogWarning($"Employee with user ID {userId} not found.");
                    return NotFound($"Employee with user ID {userId} not found.");
                }
                return Ok(employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee ID");
                return StatusCode(500, "An error occurred while fetching employee ID.");
            }
        }

        [HttpGet("fullname/{userId}")]
        public async Task<IActionResult> GetFullName(string userId)
        {
           var fullName =await _employeeService.GetEmployeFullNameByUserId(userId);

            if (fullName == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(fullName);
        }
    }
}
