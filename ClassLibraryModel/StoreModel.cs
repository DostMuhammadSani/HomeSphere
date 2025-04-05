using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;

namespace ClassLibraryModel
{
    public class StoreModel
    {
      
        public string StoreID { get; set; }

       
        public string StoreName { get; set; }

    
        public string Username { get; set; }

     
        public string Locations { get; set; }

     
        public string Passwords { get; set; }

    
        public string Contact { get; set; }

      
        public string A_id { get; set; }
    }
}