using System.ComponentModel.DataAnnotations;

namespace Project_5_final.DTO.Contact
{
    public class ContactDTO
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }
    }
}
