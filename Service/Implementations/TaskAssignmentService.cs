using EmployeeManagementSystem.Server.Constants;
using EmployeeManagementSystem.Server.Data.Dto.TaskAssignmentDTO;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TaskAssignmentService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<int> AssignTaskAsync(TaskAssignmentRequestDTO taskDto)
        {
            var inputFilePath = taskDto.InputFile != null ? await SaveFileAsync(taskDto.InputFile, "InputFiles") : null;

            var task = new TaskAssignment
            {
                EmployeeId = taskDto.EmployeeId,
                ApplicationUserId = taskDto.ApplicationUserId,
                Description = taskDto.Description,
                Deadline = taskDto.Deadline,
                InputFilePath = inputFilePath,
                Status = EmployeeTaskStatus.Pending.ToString()
            };

            await _context.TaskAssignments.AddAsync(task);
            await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task<List<TaskAssignmentResponseDTO>> GetTasksByEmployeeIdAsync(int employeeId)
        {
            return await _context.TaskAssignments
                .Where(t => t.EmployeeId == employeeId)
                .Include(t => t.Employee)
                .Select(t => new TaskAssignmentResponseDTO
                {
                    Id = t.Id,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee.FirstName,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    InputFilePath = t.InputFilePath,
                    OutputFilePath = t.OutputFilePath,
                    Status = t.Status
                }).ToListAsync();
        }

        public async Task<List<TaskAssignmentResponseDTO>> GetAllTasksAsync()
        {
            return await _context.TaskAssignments
                .Include(t => t.Employee)
                .Select(t => new TaskAssignmentResponseDTO
                {
                    Id = t.Id,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee.FirstName,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    InputFilePath = t.InputFilePath,
                    OutputFilePath = t.OutputFilePath,
                    Status = t.Status
                }).ToListAsync();
        }

        public async Task UpdateTaskStatusAsync(TaskUpdateStatusDTO updateStatusDto)
        {
            var task = await _context.TaskAssignments.FindAsync(updateStatusDto.TaskId);
            if (task == null) throw new KeyNotFoundException("Task not found");

            if (updateStatusDto.OutputFile != null)
                task.OutputFilePath = await SaveFileAsync(updateStatusDto.OutputFile, "OutputFiles");

            task.Status = updateStatusDto.Status;
            await _context.SaveChangesAsync();
        }

        private async Task<string?> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null) return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath, folderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine(folderName, uniqueFileName);
        }
    }
}
