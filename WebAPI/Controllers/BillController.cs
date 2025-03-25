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
    public class BillController : ControllerBase
    {
        // GET: api/Bill?A_id={A_id}
        [HttpGet]
        public List<BillModel> GetBillsByAdmin(string A_id)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@A_id", A_id)
            };
            return DALClass.GetDataParameter<BillModel>("GetAllBills", prm);
        }

        // POST: api/Bill
        [HttpPost]
        public async Task<IActionResult> CreateBill(BillModel bill)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@BID", bill.BID),
                new SqlParameter("@Item", bill.Item),
                new SqlParameter("@Price", bill.Price),
                new SqlParameter("@Statuss", bill.Statuss),
                new SqlParameter("@IssueDate", bill.IssueDate),
                new SqlParameter("@DueDate", bill.DueDate),
                new SqlParameter("@Fine", bill.Fine),
                new SqlParameter("@CNIC", bill.CNIC)
            };

            try
            {
                DALClass.CUDResident(p, "InsertBill");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A bill with this ID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
