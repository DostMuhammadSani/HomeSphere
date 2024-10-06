using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class TaskModel
    {
        public string Task_id { get; set; }      // Primary key
        public string Task_name { get; set; }    // Task name
        public string profession { get; set; }   // Profession
        public string A_id { get; set; }         // Foreign key referencing Admins
    }
}

