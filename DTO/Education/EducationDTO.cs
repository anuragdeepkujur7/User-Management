using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace Project_5_final.DTO.Education
{
    public class EducationDTO
    {
        [Required(ErrorMessage = "Institution name is required.")]
        public string Institution { get; set; }

        [Required(ErrorMessage = "Board is required.")]
        public string Board { get; set; }
        [Required(ErrorMessage = "Degree is required.")]
        public string Degree { get; set; }

        public string? FieldOfStudy { get; set; }

        [Range(1900, 2100, ErrorMessage = "Start date must be between 1900 and 2100.")]
        public short? StartDate { get; set; }
        [Range(1900, 2100, ErrorMessage = "End date must be between 1900 and 2100.")]
        public short? EndDate { get; set; }

        [RegularExpression(@"^[A-F]?$", ErrorMessage = "Grade must be a single letter from A to F.")]
        public string? Grade { get; set; }
    }
}
