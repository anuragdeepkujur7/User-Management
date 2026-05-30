using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_5_final.DTO.Contact;
using Project_5_final.Models;
using Project_5_final.Services.Interface;
using System.Security.Claims;

namespace Project_5_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("addcontact")]
        [Authorize]
        public async Task<IActionResult> AddContact(ContactDTO contactdto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _contactService.AddContactAsync(userId, contactdto);
                return Ok(new { message = "Conatct added succesfully" });
            }
            catch (Exception ex)
            {
                {
                    return StatusCode(500, new { error = "Unexpected error", details = ex.Message });
                }
            }
        }

         [HttpPost("update")]
         [Authorize]

         public async Task<IActionResult> UpdateContact(ContactDTO contact)
         {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (contact == null || string.IsNullOrEmpty(contact.Phone))
             {
                 return BadRequest("Invalid Contact Details");
             }
            var isUpdated = await _contactService.UpdateContactAsync(userId, contact);

            if (!isUpdated)
             {
                 return NotFound("Contact not found.");
             }

             return Ok("Contact updated successfully.");
         }
        

    }
}
