using Project_5_final.Models;
using System.Threading.Tasks;

namespace Project_5_final.Repository.Interface
{
    public interface IEducationRepository
    {
        Task<List<Education>> GetEducationByUserIdAsync(int userId);
        Task<bool> AddEducationAsync(Education education);
        Task<bool> UpdateEducationAsync(Education education);
        Task<Education?> GetEducationForUserAsync(int userId, string institution, string degree);
    }
}
