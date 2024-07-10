using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Service
{
    public Guid ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public virtual ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();
}
