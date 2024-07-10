using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomDetail { get; set; } = null!;

    public int RoomCapacity { get; set; }

    public string RoomType { get; set; } = null!;

    public int? RoomStatus { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Booked> Bookeds { get; set; } = new List<Booked>();

    public virtual ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();
}
