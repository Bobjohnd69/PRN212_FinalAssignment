using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class Report
    {
        public int? BookingReservationId { get; set; }

        public DateOnly? BookingDate { get; set; }

        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public decimal? ActualPrice { get; set; }

        public int CustomerId { get; set; }
    }
}