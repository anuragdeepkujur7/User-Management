using Project_5_final.DTO;
using Project_5_final.Models;

namespace Project_5_final.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email); 
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(int Userid);
        Task<List<User>> GetUserDataAsync(int id);
        Task<bool> UpdatePasswordAsync(User user);
        Task InvalidateTokenAsync(string token, int userId, DateTime expiration);
        Task<bool> IsTokenValidAsync(string token);
    }
}
