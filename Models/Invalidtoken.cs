using System;
using System.Collections.Generic;

namespace Project_5_final.Models;

public partial class Invalidtoken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiration { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
