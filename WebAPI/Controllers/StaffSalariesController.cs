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
    public class StaffSalariesController : ControllerBase
    {
        [HttpGet]
        public List<StaffSalaryModel> GetStaffSalaries(string A_id)
        {
            SqlParameter[] prm = { new SqlParameter("@A_id", A_id) };
            return DALClass.GetDataParameter<StaffSalaryModel>("GetStaffSalaries", prm);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaffSalary(StaffSalaryModel salary)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@SalaryID", salary.SalaryID),
                new SqlParameter("@StaffCNIC", salary.StaffCNIC),
                new SqlParameter("@Amount", salary.Amount),
                new SqlParameter("@PaymentDate", salary.PaymentDate),
                new SqlParameter("@PaymentMethod", salary.PaymentMethod),
                new SqlParameter("@Description", salary.Description),
                new SqlParameter("@A_id", salary.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "AddStaffSalary");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A salary record with this ID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaffSalary(StaffSalaryModel salary)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@SalaryID", salary.SalaryID),
                new SqlParameter("@Amount", salary.Amount),
                new SqlParameter("@PaymentDate", salary.PaymentDate),
                new SqlParameter("@PaymentMethod", salary.PaymentMethod),
                new SqlParameter("@Status", salary.Status),
                new SqlParameter("@Description", salary.Description)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateStaffSalary");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStaffSalary(string SalaryID)
        {
            SqlParameter[] p = { new SqlParameter("@SalaryID", SalaryID) };

            try
            {
                DALClass.CUDResident(p, "DeleteStaffSalary");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}