using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class ProductModel
    {
        public string ProductID { get; set; }
        public string Picture { get; set; }
        public int Price { get; set; }
        public string Descriptions { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string StoreID { get; set; }
        public string UserName { get; set; } // Needed for the operations
    }
}
