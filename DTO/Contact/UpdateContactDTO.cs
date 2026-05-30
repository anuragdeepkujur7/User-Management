using System.ComponentModel.DataAnnotations;

namespace Project_5_final.DTO.Contact
{
    public class UpdateContactDTO
    {
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
    }
}
