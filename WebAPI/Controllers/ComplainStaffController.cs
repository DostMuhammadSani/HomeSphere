using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplainStaffController : ControllerBase
    {
        // GET: api/ComplainStaff?A_id={A_id}
        [HttpGet]
        public List<ComplainStaffModel> GetComplainsByStaff(string A_id)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@A_id", A_id)
            };
            return DALClass.GetDataParameter<ComplainStaffModel>("GetComplainStaff", prm);
        }

        // POST: api/ComplainStaff
        
    }
}
