using System.ComponentModel.DataAnnotations;

namespace Project_5_final.DTO.Address
{
    //NOT IN USE
    public class UpdateAddressDTO
    {
        [Required(ErrorMessage = "Street is required.")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string? PostalCode { get; set; }
    }
}
