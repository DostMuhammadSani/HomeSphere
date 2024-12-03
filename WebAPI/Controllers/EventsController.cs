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
    public class EventsController : ControllerBase
    {
        // GET: api/Events?A_id={A_id}
        [HttpGet]
        public List<EventModels> GetEventsByAdmin(string A_id)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@A_id", A_id)
            };
            return DALClass.GetDataParameter<EventModels>("GetEvent", prm);
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> AddEvent(EventModels eventModel)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@EName", eventModel.EName),
                new SqlParameter("@Descriptions", eventModel.Descriptions),
                new SqlParameter("@Picture", eventModel.Picture),
                new SqlParameter("@EDate", eventModel.EDate),
                new SqlParameter("@A_id", eventModel.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "AddEvents");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("An event with this name already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/Events
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventModels eventModel)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@EID", eventModel.EID),
                new SqlParameter("@EName", eventModel.EName),
                new SqlParameter("@Descriptions", eventModel.Descriptions),
                new SqlParameter("@Picture", eventModel.Picture),
                new SqlParameter("@EDate", eventModel.EDate),
                new SqlParameter("@A_id", eventModel.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateEvents");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/Events?EID={EID}
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int EID)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@EID", EID)
            };

            try
            {
                DALClass.CUDResident(p, "DeleteEvents");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
