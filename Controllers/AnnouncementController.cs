using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly ILogger<AnnouncementController> _logger;

        public AnnouncementController(IAnnouncementService announcementService, ILogger<AnnouncementController> logger)
        {
            _announcementService = announcementService;
            _logger = logger;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncementsAsync();
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching announcements");
                return StatusCode(500, "An error occurred while fetching announcements.");
            }
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncementByIdAsync(id);
                if (announcement == null)
                {
                    _logger.LogWarning($"Announcement with ID {id} not found.");
                    return NotFound($"Announcement with ID {id} not found.");
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching announcement details");
                return StatusCode(500, "An error occurred while fetching announcement details.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementDto announcementDto)
        {
            if (announcementDto == null || string.IsNullOrWhiteSpace(announcementDto.Title))
                return BadRequest("Invalid announcement data.");

            try
            {
                var createdAnnouncement = await _announcementService.CreateAnnouncementAsync(announcementDto);
                return CreatedAtAction(nameof(GetAnnouncementById), new { id = createdAnnouncement.AnnouncementId }, createdAnnouncement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating announcement");
                return StatusCode(500, "An error occurred while creating the announcement.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] AnnouncementDto announcementDto)
        {
            if (announcementDto == null || string.IsNullOrWhiteSpace(announcementDto.Title))
                return BadRequest("Invalid announcement data.");

            try
            {
                var updatedAnnouncement = await _announcementService.UpdateAnnouncementAsync(id, announcementDto);
                if (updatedAnnouncement == null)
                {
                    _logger.LogWarning($"Announcement with ID {id} not found.");
                    return NotFound($"Announcement with ID {id} not found.");
                }
                return Ok(updatedAnnouncement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating announcement");
                return StatusCode(500, "An error occurred while updating the announcement.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var result = await _announcementService.DeleteAnnouncementAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"Announcement with ID {id} not found.");
                    return NotFound($"Announcement with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting announcement");
                return StatusCode(500, "An error occurred while deleting the announcement.");
            }
        }
    }
}
