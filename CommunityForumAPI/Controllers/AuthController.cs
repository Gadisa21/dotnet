using CommunityForum.Models;
using CommunityForum.Services;
using CommunityForum.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommunityForum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Injecting AuthService via constructor
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register( [FromBody] RegisterUserDto userDto)
        {
            var user=new User
            {
                Username=userDto.Username,
                Email=userDto.Email,
                Password=userDto.Password
            };
            try
            {
                // Register user and get the JWT token
                var token = await _authService.RegisterAsync(user);
                return Ok(new { Token = token }); // Return token on successful registration
            }
            catch (Exception ex)
            {
                // Handle any exception that occurs during registration
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login( string email, string password)
        {
            var token = await _authService.LoginAsync(email, password);

            if (token == null)
            {
                // Invalid login attempt
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Return the generated JWT token if login is successful
            return Ok(new { Token = token });
        }
    }
}
