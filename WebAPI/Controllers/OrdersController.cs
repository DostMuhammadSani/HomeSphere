using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/Orders?username={username}
        [HttpGet]
        public List<OrderModel> GetOrdersByStoreUsername(string username)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@Username", username)
            };
            return DALClass.GetDataParameter<OrderModel>("GetOrdersByStoreUsername", prm);
        }
    }
}