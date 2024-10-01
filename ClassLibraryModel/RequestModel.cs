using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
   

    public class RequestModel
    {
        
        public int Request_id { get; set; }

      
        public string Descriptions { get; set; }

       
        public string Statuss { get; set; }  

   
        public string R_CNIC { get; set; } // Foreign key to residents

       
        public string S_CNIC { get; set; } // Foreign key to staff

     
        public DateTime RequestTimestamp { get; set; } = DateTime.Now;

        public DateTime AcceptTimestamp { get; set; } 

        public DateTime CompletionTimestamp { get; set; } 

   
    }

}
