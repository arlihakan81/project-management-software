using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Application.Requests;

namespace PMS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController(IAuthenticateService service) : ControllerBase
    {
        private readonly IAuthenticateService _service = service;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _service.AuthenticateAsync(request.Email, request.Password);
                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _service.RegisterAsync(request);
                return Ok(new { message = "Registration successful. Please check your email to confirm your account." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
