using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    
        public class OrderTrackingModel
        {
            public string TrackingID { get; set; }
            public string OrderID { get; set; }
            public string Status { get; set; }
            public string? EstimatedDeliveryTime { get; set; }
            public string? Notes { get; set; }
            public string? RiderContact { get; set; }
        }
    }
