using System;
using System.Collections.Generic;

namespace Project_5_final.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    public int UserId { get; set; }

    public string Phone { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
