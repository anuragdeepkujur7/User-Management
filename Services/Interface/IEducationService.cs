using Project_5_final.DTO.Education;
using Project_5_final.Models;

namespace Project_5_final.Services.Interface
{
    public interface IEducationService
    {
        Task<List<Education>> GetEducationByUserIdAsync(int userId);
        Task<bool> AddEducationAsync(int userId, EducationDTO educationDTO);
        Task<bool> UpdateEducationAsync(int userId,EducationDTO educationDTO);
    }
}
