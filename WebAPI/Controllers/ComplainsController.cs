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
    public class ComplainsController : ControllerBase
    {
        // GET: api/Complains?A_id={A_id}
        [HttpGet]
        public List<ComplainsModel> GetComplainsByAdmin(string A_id)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@A_id", A_id)
            };
            return DALClass.GetDataParameter<ComplainsModel>("GetCompalins", prm);
        }

        // POST: api/Complains
        [HttpPost]
        public async Task<IActionResult> CreateComplain(ComplainsModel complain)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Complain_Description", complain.Complain_Description),
                new SqlParameter("@A_id", complain.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "CreateComplain");
                return Ok();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
