using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class BillModel
    {
        public string BID { get; set; }
        public string Item { get; set; }
        public string Price { get; set; }
        public string Statuss { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Fine { get; set; }
        public string CNIC { get; set; }
    }
}

