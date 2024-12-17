using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class EventDescriptionModel
    {
        public int EDescriptionID { get; set; } // Primary Key
        public string AudienceType { get; set; } // Audience Type (e.g., Adults, Children, etc.)
        public string VIPTicketPrice { get; set; } // VIP Ticket Price
        public string GeneralTicketPrice { get; set; } // General Ticket Price
        public string Locations { get; set; } // Locations for the event
        public int EID { get; set; } // Foreign Key referencing Eventss table
    }
}

