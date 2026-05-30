using System;
using System.Collections.Generic;

namespace Project_5_final.Models;

public partial class Education
{
    public int EducationId { get; set; }

    public int UserId { get; set; }

    public string Institution { get; set; } = null!;

    public string Board { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string? FieldOfStudy { get; set; }

    public short? EndDate { get; set; }

    public string? Grade { get; set; }

    public short? StartDate { get; set; }

    public virtual User User { get; set; } = null!;
}
