using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Service.Implementations;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class LeaveRequestController : ControllerBase
        {
           private readonly ILeaveRequestService _leaveService;

        public LeaveRequestController(ILeaveRequestService leaveService)
        {
            _leaveService = leaveService;
        }

        // ✅ POST: Employee Creates Leave Request
        [HttpPost]
            [Authorize(Roles = "Employee")]
            public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDto dto)
            {
                var result = await _leaveService.CreateLeaveRequestAsync(dto);
                return Ok(result);
            }

            // ✅ GET: Employee Views Their Leave Requests
            [HttpGet("user/{userId}")]
            [Authorize(Roles = "Employee")]
            public async Task<IActionResult> GetUserLeaveRequests(string userId)
            {
                var result = await _leaveService.GetUserLeaveRequestsAsync(userId);
                return Ok(result);
            }

            // ✅ GET: Admin Views All Leave Requests
            [HttpGet]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> GetAllLeaveRequests()
            {
                var result = await _leaveService.GetAllLeaveRequestsAsync();
                return Ok(result);
            }

            // ✅ PUT: Admin Updates Leave Status
            [HttpPut("{leaveId}/status")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> UpdateLeaveRequestStatus(int leaveId, [FromBody] string status)
            {
                var success = await _leaveService.UpdateLeaveRequestStatusAsync(leaveId, status);
                return success ? Ok("Status updated") : NotFound("Leave request not found");
            }
        }

    }

