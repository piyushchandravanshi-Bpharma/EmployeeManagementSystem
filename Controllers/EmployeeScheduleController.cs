using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeScheduleController : ControllerBase
    {
        private readonly IEmployeeScheduleService _scheduleService;

        public EmployeeScheduleController(IEmployeeScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeScheduleDto>>> GetAllSchedules()
        {
            return await _scheduleService.GetAllSchedulesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeScheduleDto>> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();

            return schedule;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeScheduleDto>> CreateSchedule(EmployeeScheduleDto scheduleDto)
        {
            var createdSchedule = await _scheduleService.CreateScheduleAsync(scheduleDto);
            return CreatedAtAction(nameof(GetSchedule), new { id = createdSchedule.ScheduleId }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, EmployeeScheduleDto scheduleDto)
        {
            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(id, scheduleDto);
            if (updatedSchedule == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var result = await _scheduleService.DeleteScheduleAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
