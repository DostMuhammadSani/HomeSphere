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
    public class EventDescriptionController : ControllerBase
    {
        // GET: api/EventDescription?EID={EID}
        [HttpGet]
        public List<EventDescriptionModel> GetEventDescriptionsByEventID(int EID)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@EID", EID)
            };
            return DALClass.GetDataParameter<EventDescriptionModel>("GetEventDescriptionsByEventID", prm);
        }

        // POST: api/EventDescription
        [HttpPost]
        public async Task<IActionResult> InsertEventDescription(EventDescriptionModel model)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@AudienceType", model.AudienceType),
                new SqlParameter("@VIPTicketPrice", model.VIPTicketPrice),
                new SqlParameter("@GeneralTicketPrice", model.GeneralTicketPrice),
                new SqlParameter("@Locations", model.Locations),
                new SqlParameter("@EID", model.EID)
            };

            try
            {
                DALClass.CUDResident(p, "InsertEventDescription");
                return Ok("Event description inserted successfully.");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("Duplicate entry detected.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/EventDescription
        [HttpPut]
        public async Task<IActionResult> UpdateEventDescription(EventDescriptionModel model)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@EDescriptionID", model.EDescriptionID),
                new SqlParameter("@AudienceType", model.AudienceType),
                new SqlParameter("@VIPTicketPrice", model.VIPTicketPrice),
                new SqlParameter("@GeneralTicketPrice", model.GeneralTicketPrice),
                new SqlParameter("@Locations", model.Locations),
                new SqlParameter("@EID", model.EID)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateEventDescription");
                return Ok("Event description updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/EventDescription?EDescriptionID={EDescriptionID}
        [HttpDelete]
        public async Task<IActionResult> DeleteEventDescription(int EDescriptionID)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@EDescriptionID", EDescriptionID)
            };

            try
            {
                DALClass.CUDResident(p, "DeleteEventDescription");
                return Ok("Event description deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
