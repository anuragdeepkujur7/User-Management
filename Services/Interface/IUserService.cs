using Project_5_final.DTO.User;
using Project_5_final.Models;

namespace Project_5_final.Services.Interface
{
    public interface IUserService
    { 
        Task<User> RegisterUserAsync(UserRegistrationDTO userDto); 
        Task<string> LoginAsync(string email, string password); 
        Task<UserResponseDTO> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(int userId, UserUpdateDTO profileUpdateDto);

        Task<string> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDTO);
        Task<string> PasswordRecoveryAsync(PasswordRecoveryDTO dto);
        Task InvalidateTokenAsync(string token, int userId, DateTime expiration);
        Task<bool> IsTokenValidAsync(string token);
    }
}

