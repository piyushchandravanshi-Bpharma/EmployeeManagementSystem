using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Service.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedbackDto>> GetAllFeedbackAsync()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();
            return feedbacks.Select(f => ConvertToDto(f));
        }

        public async Task<FeedbackDto> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            return feedback == null ? null : ConvertToDto(feedback);
        }

        public async Task<FeedbackDto> CreateFeedbackAsync(FeedbackDto feedbackDto)
        {
            var feedback = ConvertToModel(feedbackDto);
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return ConvertToDto(feedback);
        }

        public async Task<FeedbackDto> UpdateFeedbackAsync(FeedbackDto feedbackDto)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackDto.FeedbackId);
            if (feedback == null)
            {
                throw new ArgumentException("Feedback not found");
            }
            if(feedbackDto.Title != null)
            feedback.Title = feedbackDto.Title;
            if (feedbackDto.Content != null)
                feedback.Content = feedbackDto.Content;
            if (feedbackDto.Title != null)
                feedback.CreatedAt = feedbackDto.CreatedAt;

            if (feedbackDto.EmployeeId != 0)
                feedback.EmployeeId = feedbackDto.EmployeeId;

            await _context.SaveChangesAsync();
            return ConvertToDto(feedback);
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }

        private FeedbackDto ConvertToDto(Feedback feedback)
        {
            return new FeedbackDto
            {
                FeedbackId = feedback.FeedbackId,
                Title = feedback.Title,
                Content = feedback.Content,
                CreatedAt = feedback.CreatedAt,
                EmployeeId = feedback.EmployeeId
            };
        }

        private Feedback ConvertToModel(FeedbackDto feedbackDto)
        {
            return new Feedback
            {
                FeedbackId = feedbackDto.FeedbackId,
                Title = feedbackDto.Title,
                Content = feedbackDto.Content,
                CreatedAt = feedbackDto.CreatedAt,
                EmployeeId = feedbackDto.EmployeeId
            };
        }
    }


}
