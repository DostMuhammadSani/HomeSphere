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
    public class ProductsController : ControllerBase
    {
        // GET: api/Products?UserName={UserName}
        [HttpGet]
        public async Task<IActionResult> GetProductsByStore(string UserName)
        {
            

            try
            {
                var products = DALClass.GetProducts(UserName);
                if (products == null || products.Count == 0)
                {
                    return NotFound("No products found for this store or store doesn't exist");
                }
                return Ok(products);
            }
            catch (SqlException ex) when (ex.Number == 50000) // Custom error from RAISERROR
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@UserName", product.UserName),
                new SqlParameter("@ProductID", product.ProductID),
                new SqlParameter("@Picture", product.Picture),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Descriptions", product.Descriptions),
                new SqlParameter("@ExpiryDate", product.ExpiryDate)
            };

            try
            {
                DALClass.CUDResident(p, "CreateProduct");
                return Ok("Product created successfully");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A product with this ID already exists.");
            }
            catch (SqlException ex) when (ex.Number == 50000) // Custom error from RAISERROR
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/Products
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel product)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@UserName", product.UserName),
                new SqlParameter("@ProductID", product.ProductID),
                new SqlParameter("@Picture", product.Picture),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Descriptions", product.Descriptions),
                new SqlParameter("@ExpiryDate", product.ExpiryDate)
            };

            try
            {
                DALClass.CUDResident(p, "UpdateProduct");
                return Ok("Product updated successfully");
            }
            catch (SqlException ex) when (ex.Number == 50000) // Custom error from RAISERROR
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: api/Products?UserName={UserName}&ProductID={ProductID}
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string UserName, string ProductID)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@ProductID", ProductID)
            };

            try
            {
                DALClass.CUDResident(p, "DeleteProduct");
                return Ok("Product deleted successfully");
            }
            catch (SqlException ex) when (ex.Number == 50000) // Custom error from RAISERROR
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}