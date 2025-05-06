using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialReportsController : ControllerBase
    {
        [HttpGet]
        public List<FinancialReportModel> GetFinancialReports(string A_id)
        {
            SqlParameter[] prm = { new SqlParameter("@A_id", A_id) };
            return DALClass.GetDataParameter<FinancialReportModel>("GetFinancialReports", prm);
        }

        [HttpPost]
        public async Task<IActionResult> AddFinancialReport(FinancialReportModel report)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@ReportID", report.ReportID),
                new SqlParameter("@Title", report.Title),
                new SqlParameter("@Description", report.Description),
                new SqlParameter("@StartDate", report.StartDate),
                new SqlParameter("@EndDate", report.EndDate),
                new SqlParameter("@TotalIncome", report.TotalIncome),
                new SqlParameter("@TotalExpenses", report.TotalExpenses),
                new SqlParameter("@NetBalance", report.NetBalance),
                new SqlParameter("@A_id", report.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "AddFinancialReport");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A financial report with this ID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("Generate")]
        public async Task<IActionResult> GenerateFinancialReport(FinancialReportModel report)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@ReportID", report.ReportID),
                new SqlParameter("@Title", report.Title),
                new SqlParameter("@Description", report.Description),
                new SqlParameter("@StartDate", report.StartDate),
                new SqlParameter("@EndDate", report.EndDate),
                new SqlParameter("@A_id", report.A_id)
            };

            try
            {
                var result = DALClass.GetDataParameter<FinancialReportModel>("GenerateFinancialReport", p);
                return Ok(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFinancialReport(FinancialReportModel report)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@ReportID", report.ReportID),
                new SqlParameter("@Title", report.Title),
                new SqlParameter("@Description", report.Description),
                new SqlParameter("@StartDate", report.StartDate),
                new SqlParameter("@EndDate", report.EndDate),
                new SqlParameter("@TotalIncome", report.TotalIncome),
                new SqlParameter("@TotalExpenses", report.TotalExpenses),
                new SqlParameter("@NetBalance", report.NetBalance)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateFinancialReport");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFinancialReport(string ReportID)
        {
            SqlParameter[] p = { new SqlParameter("@ReportID", ReportID) };

            try
            {
                DALClass.CUDResident(p, "DeleteFinancialReport");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Summary")]
        public FinancialSummaryModel GetFinancialSummary(string A_id)
        {
            SqlParameter[] prm = { new SqlParameter("@A_id", A_id) };
            return DALClass.GetDataParameter<FinancialSummaryModel>("GetFinancialSummary", prm).FirstOrDefault();
        }
    }
}