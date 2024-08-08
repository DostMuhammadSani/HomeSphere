using ClassLibraryModel;
using DALLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {

        [HttpGet]  
        public  List<Resident> GetResidents()
        {
            return DALClass.GetData<Resident>("GetResident");

        }
        [HttpPost]
        public  async void SaveResident(Resident R)
        {
            SqlParameter[] p =
           {
                new SqlParameter("@CNIC",R.CNIC),
                new SqlParameter("@Passwords",R.Passwords),
                new SqlParameter("@Names",R.Names),
                new SqlParameter("@Contact",R.Contact),
                new SqlParameter("@A_id",R.A_id)


            };

             DALClass.CUDResident(p, "SaveResident");
        }
        [HttpPut]
        public async void UpdateResident(Resident R)
        {
            SqlParameter[] p =
           {
                new SqlParameter("@CNIC",R.CNIC),
                new SqlParameter("@Passwords",R.Passwords),
                new SqlParameter("@Names",R.Names),
                new SqlParameter("@Contact",R.Contact),
                new SqlParameter("@A_id",R.A_id)


            };

            DALClass.CUDResident(p, "UpdateResident");
        }

        [HttpDelete]
        public async void DeleteResident(string CNIC)
        {
            SqlParameter[] p =
            {
                 new SqlParameter("@CNIC",CNIC)
            };
            DALClass.CUDResident(p, "DeleteResident");
        }
    }
}
