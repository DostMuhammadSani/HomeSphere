using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnShipController : ControllerBase
    {
        // GET: api/OwnShip/{A_id}
        [HttpGet]
        public async Task<IActionResult> GetOwnShips(string A_id)
        {
            try
            {
                SqlParameter[] prm =
                {
                    new SqlParameter("@A_id",A_id)
                }; 

                var ownShips =  DALClass.GetDataParameter<OwnShipModel>("GetOwnerShip", prm);
                return Ok(ownShips);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging)
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: api/OwnShip
        [HttpPost]
        public async Task<IActionResult> InsertOwnShip([FromBody] OwnShipModel ownShip)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OwnID", ownShip.OwnID),
                new SqlParameter("@PlotID", ownShip.PlotID),
                new SqlParameter("@CNIC", ownShip.CNIC),
                new SqlParameter("@PurchaseDate", ownShip.PurchaseDate)
            };

            try
            {
                 DALClass.CUDResident(parameters, "InsertOwnerShip");
                return Ok();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key violation error number
                {
                    return BadRequest("The specified PlotID or CNIC does not exist.");
                }
                else if (ex.Number == 2627 || ex.Number == 2601) // Primary key constraint violation error numbers
                {
                    return Conflict("An ownership with this OwnID already exists.");
                }
                else
                {
                    // Handle other exceptions
                    return StatusCode(500, "An error occurred while processing your request.");
                }
            }
        }

        // PUT: api/OwnShip
        [HttpPut]
        public async Task<IActionResult> UpdateOwnShip([FromBody] OwnShipModel ownShip)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OwnID", ownShip.OwnID),
                new SqlParameter("@PlotID", ownShip.PlotID),
                new SqlParameter("@CNIC", ownShip.CNIC),
                new SqlParameter("@PurchaseDate", ownShip.PurchaseDate)
            };

            try
            {
               DALClass.CUDResident(parameters, "UpdateOwnShip");
                return Ok();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key violation error number
                {
                    return BadRequest("The specified PlotID or CNIC does not exist.");
                }
                else
                {
                    // Handle other exceptions
                    return StatusCode(500, "An error occurred while processing your request.");
                }
            }
        }

        // DELETE: api/OwnShip/{OwnID}
        [HttpDelete]
        public async Task<IActionResult> DeleteOwnShip(string OwnID)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OwnID", OwnID)
            };

            try
            {
                DALClass.CUDResident(parameters, "DeleteOwnShip");
                return Ok();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key violation error number
                {
                    return BadRequest("The specified OwnID does not exist or is referenced elsewhere.");
                }
                else
                {
                    // Handle other exceptions
                    return StatusCode(500, "An error occurred while processing your request.");
                }
            }
        }
    }
}
