using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class EventModels
    {
        public int EID { get; set; } // Event ID
        public string EName { get; set; } // Event Name
        public string Descriptions { get; set; } // Event Description
        public string? Picture { get; set; } // Picture URL or Path
        public DateTime EDate { get; set; } // Event Date
        public string StartTime { get; set; }
        public string A_id { get; set; } // Admin ID
    }
}
