using System;
using System.Collections.Generic;

namespace ticketApi.Models.Tickets
{
    public partial class EventSeat
    {
        public EventSeat()
        {
            TicketPurchaseSeat = new HashSet<TicketPurchaseSeat>();
        }

        public int EventSeatId { get; set; }
        public int SeatId { get; set; }
        public int EventId { get; set; }
        public decimal? EventSeatPrice { get; set; }

        public virtual Event Event { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual ICollection<TicketPurchaseSeat> TicketPurchaseSeat { get; set; }
    }
}
