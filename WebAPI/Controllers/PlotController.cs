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
    public class PlotController : ControllerBase
    {
        [HttpGet]
        public List<PlotModel> GetPlots(string A_id)
        {
            SqlParameter[] prm =
               {
                    new SqlParameter("@A_id",A_id)
                };
            // Fetch all plots associated with the given A_id
            return DALClass.GetDataParameter<PlotModel>("GetPlots", prm);
        }

        [HttpPost]
        public async Task<IActionResult> SavePlot(PlotModel plot)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PlotID", plot.PlotID),
                new SqlParameter("@PlotNumber", plot.PlotNumber),
                new SqlParameter("@Size", plot.Size),
                new SqlParameter("@SoldStatus", plot.SoldStatus),
                new SqlParameter("@A_id", plot.A_id)
            };

            try
            {
                // Save the plot using DALClass
                DALClass.CUDResident(parameters, "InsertPlot");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Handle primary key or unique constraint violation
                return Conflict("A plot with this PlotID already exists.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async void UpdatePlot(PlotModel plot)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PlotID", plot.PlotID),
                new SqlParameter("@PlotNumber", plot.PlotNumber),
                new SqlParameter("@Size", plot.Size),
                new SqlParameter("@SoldStatus", plot.SoldStatus)
            };

            // Update the plot using DALClass
            DALClass.CUDResident(parameters, "UpdatePlot");
        }

        [HttpDelete]
        public async void DeletePlot(string PlotID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PlotID", PlotID)
            };

            // Delete the plot using DALClass
            DALClass.CUDResident(parameters, "DeletePlot");
        }
    }
}
