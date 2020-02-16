using System;
using System.Collections.Generic;

namespace ticketApi.Models.Tickets
{
    public partial class Seat
    {
        public Seat()
        {
            EventSeat = new HashSet<EventSeat>();
        }

        public int SeatId { get; set; }
        public decimal? Price { get; set; }
        public int? RowId { get; set; }

        public virtual Row Row { get; set; }
        public virtual ICollection<EventSeat> EventSeat { get; set; }
    }
}
