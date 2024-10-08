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
    public class PlotDetailsController : ControllerBase
    {
        
        // Retrieve PlotDetails by PlotID
        [HttpGet("{plotID}")]
        public async Task<IActionResult> GetPlotDetailsByPlotID(string plotID)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@PlotID", plotID)
            };
            var plotDetails = DALClass.GetDataParameter<PlotDetailsModel>("GetPlotDetailsByPlotID", prm);
            if (plotDetails.Count!=0)
            {
                return Ok(plotDetails[0]);
            }
            else
            {
                return NotFound($"No plot details found for PlotID: {plotID}");
            }
        }

        // Create new PlotDetails
        [HttpPost]
        public async Task<IActionResult> CreatePlotDetails([FromBody] PlotDetailsModel plotDetails)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@PDID", plotDetails.PDID),
                    new SqlParameter("@Street", plotDetails.Street),
                    new SqlParameter("@Addresses", plotDetails.Addresses),
                    new SqlParameter("@HouseStatus", plotDetails.HouseStatus),
                    new SqlParameter("@Information", plotDetails.Information),
                    new SqlParameter("@PlotID", plotDetails.PlotID)
                };
                try
                {
                    DALClass.CUDResident(parameters, "AddPlotDetailsByPlotID");
                    return Ok(plotDetails);
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with this PDID already exists.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Update existing PlotDetails
        [HttpPut("{plotID}")]
        public async Task<IActionResult> UpdatePlotDetails(string plotID, [FromBody] PlotDetailsModel plotDetails)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters =
                {
                     new SqlParameter("@PlotID", plotID),
                    new SqlParameter("@Street", plotDetails.Street),
                    new SqlParameter("@Addresses", plotDetails.Addresses),
                    new SqlParameter("@HouseStatus", plotDetails.HouseStatus),
                    new SqlParameter("@Information", plotDetails.Information)
                   
                };
                try
                {
                    DALClass.CUDResident(parameters, "UpdatePlotDetailsByPlotID");
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Delete PlotDetails by PlotID
        [HttpDelete("{plotID}")]
        public async Task<IActionResult> DeletePlotDetails(string plotID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PlotID", plotID)
            };
            try
            {
                DALClass.CUDResident(parameters, "DeletePlotDetailsByPlotID");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
