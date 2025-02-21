using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = new Employee
                {
                    Email = login.Email,
                    Password = login.Password,
                };
                var result = await _authService.SignIn(data);
                if (!result.Flag)
                {   
                    return Unauthorized(result.Message);
                }
                return Ok(result);    
            }
            catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp(Employee employee)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _authService.SignUp(employee);
                if (!data.Flag)
                {
                    return StatusCode(409, new { message = data.Message });
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("decode")]
        public ActionResult DecodeToken([FromBody] string encodedToken)
        {
            try
            {
                string decryptedToken = TokenUtility.DecryptToken(encodedToken);
                return Ok(new { decryptedToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error in decoding the token", error = ex.Message });
            }
        }

    }
}
