using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        [HttpGet]
        public List<StaffModel> GetStaffByAdmin(string A_id)
        {
            SqlParameter[] prm =
   {
                    new SqlParameter("@A_id",A_id)
                };
            return DALClass.GetDataParameter<StaffModel>("GetStaffByAdmin", prm);
        }

        [HttpGet("Single")]
        public Resident GetSingle(string CNIC)
        {
            SqlParameter[] prm =
              {
                    new SqlParameter("@CNIC",CNIC)
                };
            List<Resident> R = DALClass.GetDataParameter<Resident>("GetSingleStaff", prm);
            return R[0];
        }


        [HttpPost]
        public async Task<IActionResult> SaveStaff(StaffModel S)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@CNIC", S.CNIC),
                new SqlParameter("@Passwords", S.Passwords),
                new SqlParameter("@Names", S.Names),
                new SqlParameter("@Contact", S.Contact),
                new SqlParameter("@Profession", S.Profession),
                new SqlParameter("@Picture",(object?)S.Picture ?? DBNull.Value),
                new SqlParameter("@A_id", S.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "SaveStaff");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Handle primary key or unique constraint violation
                return Conflict("A staff member with this CNIC already exists.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async void UpdateStaff(StaffModel S)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@CNIC", S.CNIC),
                new SqlParameter("@Passwords", S.Passwords),
                new SqlParameter("@Names", S.Names),
                new SqlParameter("@Contact", S.Contact),
                new SqlParameter("@Profession", S.Profession),
                new SqlParameter("@A_id", S.A_id)
            };

            DALClass.CUDResident(p, "UpdateStaff");
        }

        [HttpDelete]
        public async void DeleteStaff(string CNIC)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@CNIC", CNIC)
            };

            DALClass.CUDResident(p, "DeleteStaff");
        }
    }
}
