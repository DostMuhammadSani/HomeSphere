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
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-QQB77P7\\SQLEXPRESS;Initial Catalog=Homesphere;Integrated Security=True;");
            return con;
        }
    }
}
