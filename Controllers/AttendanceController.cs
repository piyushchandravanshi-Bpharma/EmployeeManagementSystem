using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance([FromBody] AttendanceDto attendanceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAttendance = await _attendanceService.CreateAttendanceAsync(attendanceDto);
            return CreatedAtAction(nameof(GetAttendanceById), new { id = createdAttendance.AttendanceId }, createdAttendance);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetAttendanceByEmployeeId(int employeeId)
        {
            var attendanceRecords = await _attendanceService.GetAttendanceByEmployeeIdAsync(employeeId);
            return Ok(attendanceRecords);
        }

        [HttpGet("day/{day}")]
        public async Task<IActionResult> GetAttendanceByDay(DateTime day)
        {
            var attendanceRecords = await _attendanceService.GetAttendanceByDayAsync(day);
            return Ok(attendanceRecords);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        [HttpPut("{id}/checkout")]
        public async Task<IActionResult> UpdateCheckOutStatus(int id, [FromBody] DateTime checkOut)
        {
            try
            {
                var updatedAttendance = await _attendanceService.UpdateCheckOutStatusAsync(id, checkOut);
                return Ok(updatedAttendance);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }

}
