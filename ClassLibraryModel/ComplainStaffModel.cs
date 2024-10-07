using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// ComplainStaffModel.cs
namespace ClassLibraryModel
{
    public class ComplainStaffModel
    {
        public int Complain_ID { get; set; }
        public string Complain_Description { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string RCNIC { get; set; }
        public string SCNIC { get; set; }
    }
}

