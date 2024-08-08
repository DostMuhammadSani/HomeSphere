using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpPost]
        public void Save(AdminModel p)
        {
     
            DALClass.CUD(p);
        }

        [HttpGet("AdminID")]
        public async Task<IActionResult> GetID(string HS_Name)
        {
            string ID=  DALClass.GetAdminID(HS_Name);
            if (string.IsNullOrEmpty(ID))
            {
                return BadRequest();
            }
            return Ok(ID);
        }
    }
}
