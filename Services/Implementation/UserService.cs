/*using Project_5_final.Interfaces;
using BCrypt.Net;
using Project_5_final.DTO.User;
using Project_5_final.Interfaces.Repository;*/
using Project_5_final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Project_5_final.Repository.Interface;
using Project_5_final.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Project_5_final.DTO.User;
namespace Project_5_final.Services.Implementation
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration; // For retrieving secret key from appsettings.json
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> RegisterUserAsync(UserRegistrationDTO dto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with the provided email already exists.");
            }
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DateOfBirth = null,
                Gender = null,
                
            };
            return await _userRepository.AddUserAsync(user);

        }
        public async Task<string> LoginAsync(string email, string password)   
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            // Generate JWT Token
            // return GenerateJwtToken(user);
            var claims = new[]
             {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
                );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
            return tokenValue;
        }
       
        public async Task<bool> UpdateUserProfileAsync(int userId, UserUpdateDTO profileUpdateDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return false; 
            }

            user.FirstName=profileUpdateDto.FirstName;
            user.LastName=profileUpdateDto.LastName;
            user.Gender = profileUpdateDto.Gender;
            
            user.UpdatedOn = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<UserResponseDTO> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserDataAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var userDetails = user.FirstOrDefault();

            if (userDetails == null)
            {
                throw new KeyNotFoundException("User details not found.");
            }

            return new UserResponseDTO
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Email = userDetails.Email,
                DateOfBirth = userDetails.DateOfBirth,
                Gender = userDetails.Gender,
                Street = userDetails.Addresses?.FirstOrDefault()?.Street,
                PostalCode = userDetails.Addresses?.FirstOrDefault()?.PostalCode,
                City = userDetails.Addresses?.FirstOrDefault()?.City,
                State = userDetails.Addresses?.FirstOrDefault()?.State,
                Country = userDetails.Addresses?.FirstOrDefault()?.Country,
                Phone = userDetails.Contacts?.FirstOrDefault()?.Phone,
                Institution = userDetails.Educations?.FirstOrDefault()?.Institution,
                Board = userDetails.Educations?.FirstOrDefault()?.Board,
                Degree = userDetails.Educations?.FirstOrDefault()?.Degree,
                FieldOfStudy = userDetails.Educations?.FirstOrDefault()?.FieldOfStudy,
                Grade = userDetails.Educations?.FirstOrDefault()?.Grade
            };
        }

        public async Task<string> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return "User not found.";
            }

            
            if (!BCrypt.Net.BCrypt.Verify(changePasswordDTO.CurrentPassword, user.PasswordHash))
            {
                return "Current password is incorrect.";
            }

           
          

            if (changePasswordDTO.NewPassword != changePasswordDTO.ConfirmPassword)
            {
                return "New password and confirmation password do not match.";
            }

            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword);

            
            var isUpdated = await _userRepository.UpdatePasswordAsync(user);

            if (!isUpdated)
            {
                return "Error updating password. Please try again.";
            }

            return "Password changed successfully.";
        }
       
        public async Task<string> PasswordRecoveryAsync(PasswordRecoveryDTO dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user != null)
            {
                
                return "Password recovery email sent.";
            }
            return "Email not found in the system.";
        }
       
        public async Task InvalidateTokenAsync(string token, int userId, DateTime expiration)
        {
            await _userRepository.InvalidateTokenAsync(token, userId, expiration);
        }

        // Method to check token validity
        public async Task<bool> IsTokenValidAsync(string token)
        {
            return await _userRepository.IsTokenValidAsync(token);
        }
    }
}
