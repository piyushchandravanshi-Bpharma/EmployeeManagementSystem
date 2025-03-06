using EmployeeManagementSystem.Server.Data.Dto;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackDto>> GetAllFeedbackAsync();
        Task<FeedbackDto> GetFeedbackByIdAsync(int id);
        Task<FeedbackDto> CreateFeedbackAsync(FeedbackDto feedbackDto);
        Task<FeedbackDto> UpdateFeedbackAsync(FeedbackDto feedbackDto);
        Task DeleteFeedbackAsync(int id);
    }
}
