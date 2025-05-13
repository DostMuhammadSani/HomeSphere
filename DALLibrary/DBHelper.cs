using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLibrary
{
    public class DBHelper
    {
        public static SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=82.112.236.203,1401;Initial Catalog=Homesphere;User ID=sa;Password=Awab123@@A");
            return con;
        }
    }
}
