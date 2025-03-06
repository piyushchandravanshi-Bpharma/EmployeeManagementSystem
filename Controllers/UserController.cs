using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;


        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _userService.CreateEmployeeAsync(employeeDTO);
                return CreatedAtAction(nameof(CreateUser), new { id = createdUser.EmployeeId }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("count/{roleName}")]
        public async Task<IActionResult> GetUserCountInRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return NotFound($"Role '{roleName}' does not exist.");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            int count = usersInRole.Count();

            return Ok(new { Role = roleName, UserCount = count });
        }
    }
}
