using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_5_final.DTO.Address;
using Project_5_final.Models;
using Project_5_final.Services.Interface;
using System.Security.Claims;

namespace Project_5_final.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class AddressController : ControllerBase
    {
       
        //Latest
        public readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddAddress([FromBody] AddressCreateDTO addressDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _addressService.AddAddressAsync(userId, addressDto);
                return Ok(new { Message = "Address added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the address.", Error = ex.InnerException });
            }
        }

        
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressCreateDTO addressDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _addressService.UpdateAddressAsync(userId,addressDto);
                return Ok(new { Message = "Address updated successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the address.", Error = ex.Message });
            }
        }              
    }
}
