using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {

        [HttpGet]  
        public  List<Resident> GetResidents(string A_id)
        {
            return DALClass.GetDataParameter<Resident>("GetResidents",A_id);

        }
        [HttpGet("Count")]
        public List<CountModel> GetCount(string A_id)
        {
            return DALClass.GetDataParameter<CountModel>("CountRecords", A_id);

        }
        [HttpPost]
        public  async Task<IActionResult> SaveResident(Resident R)
        {
            SqlParameter[] p =
           {
                new SqlParameter("@CNIC",R.CNIC),
                new SqlParameter("@Passwords",R.Passwords),
                new SqlParameter("@Names",R.Names),
                new SqlParameter("@Contact",R.Contact),
                new SqlParameter("@A_id",R.A_id)


            };
            try
            {

                DALClass.CUDResident(p, "SaveResident");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Handle primary key or unique constraint violation
                return Conflict("A resident with this CNIC already exists.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut]
        public async void UpdateResident(Resident R)
        {
            SqlParameter[] p =
           {
                new SqlParameter("@CNIC",R.CNIC),
                new SqlParameter("@Passwords",R.Passwords),
                new SqlParameter("@Names",R.Names),
                new SqlParameter("@Contact",R.Contact),
                new SqlParameter("@A_id",R.A_id)


            };

            DALClass.CUDResident(p, "UpdateResident");
        }

        [HttpDelete]
        public async void DeleteResident(string CNIC)
        {
            SqlParameter[] p =
            {
                 new SqlParameter("@CNIC",CNIC)
            };
            DALClass.CUDResident(p, "DeleteResident");
        }
    }
}
