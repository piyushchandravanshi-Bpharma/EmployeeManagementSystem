using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Service.Implementations;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        // GET: api/department
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching departments");
                return StatusCode(500, "An error occurred while fetching departments.");
            }
        }

        // GET: api/department/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null)
                {
                    _logger.LogWarning($"Department with ID {id} not found.");
                    return NotFound($"Department with ID {id} not found.");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching department details");
                return StatusCode(500, "An error occurred while fetching department details.");
            }
        }

        // POST: api/department
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null || string.IsNullOrWhiteSpace(departmentDto.DepartmentName))
                return BadRequest("Invalid department data.");

            try
            {
                var createdDepartment = await _departmentService.CreateDepartmentAsync(departmentDto);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepartmentId }, createdDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department");
                return StatusCode(500, "An error occurred while creating the department.");
            }
        }

        // PUT: api/department/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null || string.IsNullOrWhiteSpace(departmentDto.DepartmentName))
                return BadRequest("Invalid department data.");

            try
            {
                var updatedDepartment = await _departmentService.UpdateDepartmentAsync(id, departmentDto);
                if (updatedDepartment == null)
                {
                    _logger.LogWarning($"Department with ID {id} not found.");
                    return NotFound($"Department with ID {id} not found.");
                }
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating department");
                return StatusCode(500, "An error occurred while updating the department.");
            }
        }

        // DELETE: api/department/{id}

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                bool deleted = await _departmentService.DeleteDepartmentAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning($"Department with ID {id} not found.");
                    return NotFound($"Department with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting department");
                return StatusCode(500, "An error occurred while deleting the department.");
            }
        }
    }
}
