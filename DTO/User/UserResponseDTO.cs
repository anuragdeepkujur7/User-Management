namespace Project_5_final.DTO.User
{
    public class UserResponseDTO
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        //Modification
        public string? Institution { get; set; }
        public string? Board { get; set; }

        public string? Degree { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? Grade { get; set; }



    }
}
