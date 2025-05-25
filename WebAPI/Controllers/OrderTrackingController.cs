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
    public class OrderTrackingController : ControllerBase
    {
        // GET: api/OrderTracking?OrderID={OrderID}
        [HttpGet]
        public List<OrderTrackingModel> GetOrderTrackingByOrderID(string OrderID)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@OrderID", OrderID)
            };
            return DALClass.GetDataParameter<OrderTrackingModel>("GetOrderTrackingByOrderID", prm);
        }

        // POST: api/OrderTracking
        [HttpPost]
        public async Task<IActionResult> CreateOrderTracking(OrderTrackingModel orderTracking)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@TrackingID", orderTracking.TrackingID),
                new SqlParameter("@OrderID", orderTracking.OrderID),
                new SqlParameter("@Status", orderTracking.Status),
                new SqlParameter("@EstimatedDeliveryTime", orderTracking.EstimatedDeliveryTime ?? (object)DBNull.Value),
                new SqlParameter("@Notes", orderTracking.Notes ?? (object)DBNull.Value),
                new SqlParameter("@RiderContact", orderTracking.RiderContact ?? (object)DBNull.Value)
            };

            try
            {
                DALClass.CUDResident(p, "CreateOrderTracking");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("An order tracking record with this TrackingID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/OrderTracking
        [HttpPut]
        public async Task<IActionResult> UpdateOrderTracking(OrderTrackingModel orderTracking)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@TrackingID", orderTracking.TrackingID),
                new SqlParameter("@OrderID", orderTracking.OrderID),
                new SqlParameter("@Status", orderTracking.Status),
                new SqlParameter("@EstimatedDeliveryTime", orderTracking.EstimatedDeliveryTime ?? (object)DBNull.Value),
                new SqlParameter("@Notes", orderTracking.Notes ?? (object)DBNull.Value),
                new SqlParameter("@RiderContact", orderTracking.RiderContact ?? (object)DBNull.Value)
            };

            try
            {
                DALClass.CUDResident(p, "sp_UpdateOrderTracking");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/OrderTracking?TrackingID={TrackingID}
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderTracking(string TrackingID)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@TrackingID", TrackingID)
            };

            try
            {
                DALClass.CUDResident(p, "sp_DeleteOrderTracking");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}