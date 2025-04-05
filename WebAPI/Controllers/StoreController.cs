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
    public class StoresController : ControllerBase
    {
        // GET: api/Stores?A_id={A_id}
        [HttpGet]
        public List<StoreModel> GetStoresByAdmin(string A_id)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@A_id", A_id)
            };
            return DALClass.GetDataParameter<StoreModel>("GetStoresByAdminID", prm);
        }

        // POST: api/Stores
        [HttpPost]
        public async Task<IActionResult> CreateStore(StoreModel store)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@StoreID", store.StoreID),
                new SqlParameter("@StoreName", store.StoreName),
                new SqlParameter("@Username", store.Username),
                new SqlParameter("@Locations", store.Locations),
                new SqlParameter("@Passwords", store.Passwords),
                new SqlParameter("@Contact", store.Contact),
                new SqlParameter("@A_id", store.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "InsertStore");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A store with this ID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/Stores
        [HttpPut]
        public async Task<IActionResult> UpdateStore(StoreModel store)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@StoreID", store.StoreID),
                new SqlParameter("@StoreName", store.StoreName),
                new SqlParameter("@Username", store.Username),
                new SqlParameter("@Locations", store.Locations),
                new SqlParameter("@Passwords", store.Passwords),
                new SqlParameter("@Contact", store.Contact)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateStore");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/Stores?StoreID={StoreID}
        [HttpDelete]
        public async Task<IActionResult> DeleteStore(string StoreID)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@StoreID", StoreID)
            };

            try
            {
                DALClass.CUDResident(p, "DeleteStore");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}