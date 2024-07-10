using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class RoomService
{
    public int RoomId { get; set; }

    public Guid ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public int? Status { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
