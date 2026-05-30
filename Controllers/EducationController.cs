using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_5_final.DTO.Education;
using Project_5_final.Models;
using Project_5_final.Services.Implementation;
using Project_5_final.Services.Interface;
using System.Security.Claims;

namespace Project_5_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpPost("addeducation")]
        [Authorize]
        public async Task<IActionResult> AddEducation([FromBody] EducationDTO educationDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _educationService.AddEducationAsync(userId, educationDto);
            if (!result)
            {
                return BadRequest("Education record not found or does not belong to the user.");
            }

            return Ok("Education added successfully.");
        }

        [HttpPut("updateeducation")]
        [Authorize]
        public async Task<IActionResult> UpdateEducation([FromBody] EducationDTO educationDTO)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _educationService.UpdateEducationAsync(userId, educationDTO);

            if (!result)
            {
                return NotFound("Education record not found.");
            }

            return Ok("Updated in DB"); 
        }
    }
}



