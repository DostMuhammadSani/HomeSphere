using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class StaffSalaryModel
    {
        public string SalaryID { get; set; }
        public string StaffCNIC { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string A_id { get; set; }
       
    }
}
