using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ComplainsModel.cs
namespace ClassLibraryModel
{
    public class ComplainsModel
    {
        public int Complain_ID { get; set; }
        public string Complain_Description { get; set; }
        public string CNIC { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string A_id { get; set; }
    }
}




