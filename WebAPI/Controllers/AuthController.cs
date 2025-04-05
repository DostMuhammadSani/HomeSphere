using ClassLibraryModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("registeradmin")]
        public async Task<IActionResult> Register([FromBody] AdminModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser { UserName = model.HS_Name, Email = model.HS_Name };
            var result = await _userManager.CreateAsync(user, model.Passwords);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "ADMIN");
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                return Ok(new { message = "Registration successful" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("registerShopkeeper")]
        public async Task<IActionResult> RegisterShopkeeper([FromBody] StoreModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser { UserName = model.Username, Email = model.Username };
            var result = await _userManager.CreateAsync(user, model.Passwords);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Shopkeeper");
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                return Ok(new { message = "Registration successful" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("updateadmin/{username}")]
        public async Task<IActionResult> UpdateAdmin(string username, [FromBody] AdminModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "Admin user not found." });
            }

            // Update Email (username/HS_Name)
            user.UserName = model.HS_Name;
            user.Email = model.HS_Name;

            var result = await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(model.Passwords))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Passwords);
                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors);
                }
            }

            if (result.Succeeded)
            {
                return Ok(new { message = "Admin user updated successfully." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("updateshopkeeper/{username}")]
        public async Task<IActionResult> UpdateShopkeeper(string username, [FromBody] StoreModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "Shopkeeper user not found." });
            }

            user.UserName = model.Username;
            user.Email = model.Username;

            var result = await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(model.Passwords))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Passwords);
                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors);
                }
            }

            if (result.Succeeded)
            {
                return Ok(new { message = "Shopkeeper user updated successfully." });
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete("deleteadmin/{username}")]
        public async Task<IActionResult> DeleteAdmin(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "Admin user not found." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("ADMIN"))
            {
                return BadRequest(new { message = "User is not an admin." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "Admin deleted successfully." });
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("deleteshopkeeper/{username}")]
        public async Task<IActionResult> DeleteShopkeeper(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "Shopkeeper user not found." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Shopkeeper"))
            {
                return BadRequest(new { message = "User is not a shopkeeper." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "Shopkeeper deleted successfully." });
            }

            return BadRequest(result.Errors);
        }


        [HttpPost("loginadmin")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var role = await _userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user, role);
                return Ok(new { Token = token });
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new { message = "User account locked out." });
            }

            return BadRequest(new { message = "Invalid login attempt." });
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
