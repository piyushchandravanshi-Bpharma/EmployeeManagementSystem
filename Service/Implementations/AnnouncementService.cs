using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Server.Service
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync()
        {
            return await _context.Announcements
                .Select(a => new AnnouncementDto
                {
                    AnnouncementId = a.AnnouncementId,
                    Title = a.Title,
                    Content = a.Content,
                    CreatedAt = a.CreatedAt,
                    CreatedBy = a.CreatedBy
                }).ToListAsync();
        }

        public async Task<AnnouncementDto> GetAnnouncementByIdAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return null;

            return new AnnouncementDto
            {
                AnnouncementId = announcement.AnnouncementId,
                Title = announcement.Title,
                Content = announcement.Content,
                CreatedAt = announcement.CreatedAt,
                CreatedBy = announcement.CreatedBy
            };
        }

        public async Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementDto announcementDto)
        {
            var announcement = new Announcement
            {
                Title = announcementDto.Title,
                Content = announcementDto.Content,
                CreatedBy = announcementDto.CreatedBy
            };

            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            announcementDto.AnnouncementId = announcement.AnnouncementId;
            return announcementDto;
        }

        public async Task<AnnouncementDto> UpdateAnnouncementAsync(int id, AnnouncementDto announcementDto)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return null;

            announcement.Title = announcementDto.Title;
            announcement.Content = announcementDto.Content;
            announcement.CreatedBy = announcementDto.CreatedBy;

            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();

            return announcementDto;
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return false;

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
