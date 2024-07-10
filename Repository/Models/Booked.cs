using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Booked
{
    public Guid BookedId { get; set; }

    public int RoomNumber { get; set; }

    public string GuessName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal Cost { get; set; }

    public int? Status { get; set; }

    public virtual Room RoomNumberNavigation { get; set; } = null!;
}
