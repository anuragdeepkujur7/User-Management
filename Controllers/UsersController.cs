using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_5_final.DTO.User;

//using Project_5_final.Helper;
using Project_5_final.Models;
using Project_5_final.Services.Implementation;
using Project_5_final.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace Project_5_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;

        }
       
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO userDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(userDto);
              // return Ok(new { message = "User registered successfully", data = user });
              return Ok(user);

            }
            catch (InvalidOperationException ex)
            {
               // return BadRequest(new { error = ex.Message });
               return BadRequest(ex.Message);
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        { 
            try
            {
                var token = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
                return Ok(new { token });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult> ViewProfile()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var profile = await _userService.GetUserProfileAsync(userId);

                return Ok(profile);
            }
            catch (KeyNotFoundException ex)
            {
                
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("updateprofile")]
        [Authorize]  
        public async Task<ActionResult> UpdateProfile([FromBody] UserUpdateDTO profileUpdateDTO)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _userService.UpdateUserProfileAsync(userId, profileUpdateDTO);

            if (!result)
            {
                return BadRequest("Failed to update profile.");
            }

            return Ok(new { message = "Updated" });
        }



        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _userService.ChangePasswordAsync(userId, changePasswordDTO);

            if (result == "Password changed successfully.")
            {
                return Ok(new { Message = result });
            }

            return BadRequest(new { Message = result });
        }


        [HttpPost("password-recovery")]
        public async Task<IActionResult> PasswordRecovery([FromBody] PasswordRecoveryDTO dto)
        {
            var message = await _userService.PasswordRecoveryAsync(dto);
            return Ok(new { Message = message });
        }
        
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("User is not logged in.");
            }

            var expiration = DateTime.UtcNow.AddMinutes(30); 
            await _userService.InvalidateTokenAsync(token, int.Parse(userId), expiration);

            return Ok("Logged out successfully.");
        }

        
    }
}

   
