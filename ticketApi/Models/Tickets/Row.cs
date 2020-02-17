using System;
using System.Collections.Generic;

namespace ticketApi.Models.Tickets
{
    public partial class Row
    {
        public Row()
        {
            Seat = new HashSet<Seat>();
        }

        public int RowId { get; set; }
        public string RowName { get; set; }
        public int? SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
