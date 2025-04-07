using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class OrderModel
    {
        public string OrderID { get; set; }
        public DateTime OrderTime { get; set; }
        public float TotalAmount { get; set; }
        public float DeliveryCharges { get; set; }
        public string DeliveryLocation { get; set; }
        public string CNIC { get; set; }
        public string StoreID { get; set; }
    }
}
