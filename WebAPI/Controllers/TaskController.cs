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
    public class TaskController : ControllerBase
    {
        // GET: api/Task?A_id={A_id}
        [HttpGet]
        public List<TaskModel> GetTasksByAdmin(string A_id)
        {
            SqlParameter[] prm =
            {
                new SqlParameter("@A_id", A_id)
            };
            return DALClass.GetDataParameter<TaskModel>("GetAllTasks", prm);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Task_id", task.Task_id),
                new SqlParameter("@Task_name", task.Task_name),
                new SqlParameter("@profession", task.profession),
                new SqlParameter("@A_id", task.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "CreateTask");
                return Ok();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A task with this ID already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/Task
        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskModel task)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Task_id", task.Task_id),
                new SqlParameter("@Task_name", task.Task_name),
                new SqlParameter("@profession", task.profession),
                new SqlParameter("@A_id", task.A_id)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateTask");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/Task?Task_id={Task_id}
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(string Task_id)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Task_id", Task_id)
            };

            try
            {
                DALClass.CUDResident(p, "DeleteTask");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
