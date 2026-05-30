using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_5_final.DTO;
using Project_5_final.Models;
using System.Security.Claims;

namespace Project_5_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploymentController : ControllerBase
    {
        private readonly UserdbContext _context;

        public EmploymentController(UserdbContext context)
        {
            _context = context;
        }

        [HttpPost("addemployment")]
        [Authorize]
        public async Task<IActionResult> AddEmployment(DTO.Employment.UpdateEmploymentDTO employmentDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var employment = new Employment
            {
                UserId = userId,
                Company = employmentDto.Company,
                JobTitle = employmentDto.JobTitle,
                StartDate = employmentDto.StartDate,
                EndDate = employmentDto.EndDate,
                Salary = employmentDto.Salary
            };

            _context.Employments.Add(employment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Employment added successfully" });
        }

        // Add more endpoints as needed(update employment)
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateEmployment(DTO.Employment.UpdateEmploymentDTO employmentDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var employment= await _context.Employments.FirstOrDefaultAsync(e=>e.UserId == userId);
            if (employment == null) { 
                return NotFound(new {message="Employment details not found"});
            }

            if (!string.IsNullOrWhiteSpace(employment.Company)) { 
                employment.Company = employmentDto.Company;
            }
            if (!string.IsNullOrWhiteSpace(employmentDto.JobTitle))
                employment.JobTitle = employmentDto.JobTitle;

            /*if (employmentDto.StartDate.HasValue)
                employment.StartDate = employmentDto.StartDate.value;

            if (employmentDto.EndDate.HasValue)
                employment.EndDate = employmentDto.EndDate.Value;

            if (employmentDto.Salary.HasValue)
                employment.Salary = employmentDto.Salary.Value;*/


            await _context.Employments.AddAsync(employment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Employment details updated successfully" });

        }
    }

}
