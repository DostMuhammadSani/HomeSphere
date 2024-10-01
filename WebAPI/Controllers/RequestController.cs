using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassLibraryModel;
using System.Data.SqlClient;
using DALLibrary;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        [HttpGet]
        public List<RequestModel> GetRequests(string A_id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@A_id",A_id) };

            return DALClass.GetDataParameter<RequestModel>("GetRequests", parameters);
        
        }
        
    }
}
