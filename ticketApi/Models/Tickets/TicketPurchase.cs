using System;
using System.Collections.Generic;

namespace ticketApi.Models.Tickets
{
    public partial class TicketPurchase
    {
        public TicketPurchase()
        {
            TicketPurchaseSeat = new HashSet<TicketPurchaseSeat>();
        }

        public int PurchaseId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public string ConfirmationCode { get; set; }

        public virtual ICollection<TicketPurchaseSeat> TicketPurchaseSeat { get; set; }
    }
}
