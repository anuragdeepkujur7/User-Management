namespace Project_5_final.DTO.Employment
{
    public class UpdateEmploymentDTO
    {
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Salary { get; set; }
    }
}
