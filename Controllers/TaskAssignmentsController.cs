using EmployeeManagementSystem.Server.Data.Dto.TaskAssignmentDTO;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly ITaskAssignmentService _taskService;

        public TaskAssignmentsController(ITaskAssignmentService taskService)
        {
            _taskService = taskService;
        }

        // Admin: Assign Task
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTask([FromForm] TaskAssignmentRequestDTO taskDto)
        {
            var taskId = await _taskService.AssignTaskAsync(taskDto);
            return Ok(new { Id = taskId, Message = "Task assigned successfully" });
        }

        // Admin: View All Tasks
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // Employee: View Tasks By Employee ID
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetTasksByEmployeeId(int employeeId)
        {
            var tasks = await _taskService.GetTasksByEmployeeIdAsync(employeeId);
            return Ok(tasks);
        }

        // Employee: Update Task Status
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateTaskStatus([FromForm] TaskUpdateStatusDTO updateStatusDto)
        {
            await _taskService.UpdateTaskStatusAsync(updateStatusDto);
            return Ok(new { Message = "Task status updated successfully" });
        }
    }
}
