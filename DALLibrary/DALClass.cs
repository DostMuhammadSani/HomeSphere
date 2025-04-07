using ClassLibraryModel;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DALLibrary
{
    public class DALClass
    {
        public static void CUD ( AdminModel p)
        {
            SqlConnection conn = DBHelper.getConnection();
            conn.Open ();
            SqlCommand cmd= new SqlCommand ("SaveAdmin", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@A_id", p.A_id);
            cmd.Parameters.AddWithValue("@HS_Name", p.HS_Name);
            cmd.Parameters.AddWithValue("@Passwords",ComputeSha256Hash( p.Passwords));
            cmd.ExecuteNonQuery ();
            conn.Close ();
        }
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public static void CUDResident(SqlParameter[] parameters, string procdeureName )
        {
           



            SqlConnection conn = DBHelper.getConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand(procdeureName, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
            conn.Close();

        }


        public static string GetAdminID(string HS_Name)
        {
            string A_Id="";
            SqlConnection conn = DBHelper.getConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetAdminID", conn);
            cmd.CommandType= System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HS_Name", HS_Name);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               
                A_Id = Convert.ToString(reader["A_Id"]);
   
            }
            conn.Close();
            return A_Id;


        }


        public static string GetPlotID(string PlotNumber)
        {
            string PlotID = "";
            SqlConnection conn = DBHelper.getConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetPlotID", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PlotNumber", PlotNumber);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                PlotID = Convert.ToString(reader["PlotID"]);

            }
            conn.Close();
            return PlotID;


        }



        public static List<T> GetData<T>(string procedureName) where T : class , new()
        {

            List<T> users = new List<T>();
            SqlConnection con = DBHelper.getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            Type tp = typeof(T);
            PropertyInfo[] properties = tp.GetProperties();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                T obj = new T();
                foreach (var property in properties)
                {

                    var name = property.Name;

                    if (reader[name] != DBNull.Value)
                    {
                        property.SetValue(obj, Convert.ChangeType(reader[name], property.PropertyType));
                    }
                    else
                    {
                        // Assign a default value if the property is a reference type or nullable
                        if (property.PropertyType.IsClass || (Nullable.GetUnderlyingType(property.PropertyType) != null))
                        {
                            property.SetValue(obj, null);
                        }
                        else
                        {
                            // Assign a default value for value types (int, decimal, etc.)
                            property.SetValue(obj, Activator.CreateInstance(property.PropertyType));
                        }
                    }



                }

                users.Add(obj);

            }
            con.Close();
            return users;





        }




        public static List<T> GetDataParameter<T>(string procedureName,SqlParameter[] prm) where T : class, new()
        {

            List<T> users = new List<T>();
            SqlConnection con = DBHelper.getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.Parameters.AddRange(prm);
            cmd.CommandType = CommandType.StoredProcedure;
            Type tp = typeof(T);
            PropertyInfo[] properties = tp.GetProperties();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                T obj = new T();
                foreach (var property in properties)
                {

                    var name = property.Name;

                    if (reader[name] != DBNull.Value)
                    {
                        property.SetValue(obj, Convert.ChangeType(reader[name], property.PropertyType));
                    }
                    else
                    {
                        // Assign a default value if the property is a reference type or nullable
                        if (property.PropertyType.IsClass || (Nullable.GetUnderlyingType(property.PropertyType) != null))
                        {
                            property.SetValue(obj, null);
                        }
                        else
                        {
                            // Assign a default value for value types (int, decimal, etc.)
                            property.SetValue(obj, Activator.CreateInstance(property.PropertyType));
                        }
                    }
                


            }

                users.Add(obj);

            }
            con.Close();
            return users;





        }


        public static List<ProductModel> GetProducts(string UserName)
        {
            List<ProductModel> Products = new List<ProductModel>();
            SqlConnection conn = DBHelper.getConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetProducts", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", UserName);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ProductModel P = new ProductModel();
                P.ProductID = Convert.ToString(reader["ProductID"]);
                P.Picture = Convert.ToString(reader["Picture"]);
                P.Price = (int)reader["Price"];
                P.Descriptions = Convert.ToString(reader["Descriptions"]);
                P.ExpiryDate = (DateTime)reader["ExpiryDate"]; 
                P.StoreID = Convert.ToString(reader["StoreID"]);
                Products.Add(P);
            }
            conn.Close();
            return Products;


        }


    


    public static List<ProductModel> GetProductwithOrders(string OrderID)
        {
            List<ProductModel> Products = new List<ProductModel>();
            SqlConnection conn = DBHelper.getConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetProductsByOrderID", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderID ", OrderID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ProductModel P = new ProductModel();
                P.ProductID = Convert.ToString(reader["ProductID"]);
                P.Picture = Convert.ToString(reader["Picture"]);
                P.Price = (int)reader["Price"];
                P.Descriptions = Convert.ToString(reader["Descriptions"]);
                P.ExpiryDate = (DateTime)reader["ExpiryDate"];
                P.StoreID = Convert.ToString(reader["StoreID"]);
                Products.Add(P);
            }
            conn.Close();
            return Products;


        }


    }

}
