using EmployeeManagementSystem.Server.Data.Dto;
using EmployeeManagementSystem.Server.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Server.Service.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync();
        Task<AnnouncementDto> GetAnnouncementByIdAsync(int id);
        Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementDto announcementDto);
        Task<AnnouncementDto> UpdateAnnouncementAsync(int id, AnnouncementDto announcementDto);
        Task<bool> DeleteAnnouncementAsync(int id);
    }
}
