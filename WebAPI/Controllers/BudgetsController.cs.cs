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
    public class BudgetsController : ControllerBase
    {
        [HttpGet]
        public List<BudgetModel> GetBudgets(string A_id)
        {
            SqlParameter[] prm = { new SqlParameter("@A_id", A_id) };
            return DALClass.GetDataParameter<BudgetModel>("GetBudgets", prm);
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget(BudgetModel budget)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@BudgetID", budget.BudgetID),
                new SqlParameter("@Title", budget.Title),
                new SqlParameter("@Description", budget.Description),
                new SqlParameter("@StartDate", budget.StartDate),
                new SqlParameter("@EndDate", budget.EndDate),
                new SqlParameter("@TotalAmount", budget.TotalAmount),
                new SqlParameter("@A_id", budget.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "AddBudget");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A budget with this ID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBudget(BudgetModel budget)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@BudgetID", budget.BudgetID),
                new SqlParameter("@Title", budget.Title),
                new SqlParameter("@Description", budget.Description),
                new SqlParameter("@StartDate", budget.StartDate),
                new SqlParameter("@EndDate", budget.EndDate),
                new SqlParameter("@TotalAmount", budget.TotalAmount),
                new SqlParameter("@Status", budget.Status)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateBudget");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBudget(string BudgetID)
        {
            SqlParameter[] p = { new SqlParameter("@BudgetID", BudgetID) };

            try
            {
                DALClass.CUDResident(p, "DeleteBudget");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}