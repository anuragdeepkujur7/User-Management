using System;
using System.Collections.Generic;

namespace Project_5_final.Models;

public partial class Employment
{
    public int EmploymentId { get; set; }

    public int UserId { get; set; }

    public string Company { get; set; } = null!;

    public string? JobTitle { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? Salary { get; set; }

    public virtual User User { get; set; } = null!;
}
