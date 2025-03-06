using EmployeeManagementSystem.Server.Data.Dto.TaskAssignmentDTO;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface ITaskAssignmentService
    {
        Task<int> AssignTaskAsync(TaskAssignmentRequestDTO taskDto);
        Task<List<TaskAssignmentResponseDTO>> GetTasksByEmployeeIdAsync(int employeeId);
        Task<List<TaskAssignmentResponseDTO>> GetAllTasksAsync();
        Task UpdateTaskStatusAsync(TaskUpdateStatusDTO updateStatusDto);
    }
}
