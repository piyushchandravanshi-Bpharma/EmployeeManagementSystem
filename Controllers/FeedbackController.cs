using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            var feedbacks = await _feedbackService.GetAllFeedbackAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFeedback = await _feedbackService.CreateFeedbackAsync(feedbackDto);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.FeedbackId }, createdFeedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] FeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid || id != feedbackDto.FeedbackId)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(feedbackDto);
                return Ok(updatedFeedback);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            await _feedbackService.DeleteFeedbackAsync(id);
            return NoContent();
        }
    }

}
