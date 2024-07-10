using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Role { get; set; } = null!;

    public int? Status { get; set; }

    public string Password { get; set; } = null!;
}
