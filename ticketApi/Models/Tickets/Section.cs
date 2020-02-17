using System;
using System.Collections.Generic;

namespace ticketApi.Models.Tickets
{
    public partial class Section
    {
        public Section()
        {
            Row = new HashSet<Row>();
        }

        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string VenueName { get; set; }

        public virtual Venue VenueNameNavigation { get; set; }
        public virtual ICollection<Row> Row { get; set; }
    }
}
