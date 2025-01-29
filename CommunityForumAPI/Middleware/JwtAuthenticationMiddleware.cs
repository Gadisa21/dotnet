using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace CommunityForum.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Retrieve the token from the Authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var keyString = _configuration["Jwt:Key"];
                    if (string.IsNullOrEmpty(keyString))
                    {
                        throw new InvalidOperationException("JWT key is missing in configuration.");
                    }
                    // Validate the token using the key (no issuer or audience required)
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                    var handler = new JwtSecurityTokenHandler();
                    var claimsPrincipal = handler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = false, // Skip issuer validation
                        ValidateAudience = false, // Skip audience validation
                        ValidateLifetime = true, // Validate token expiration
                        ValidateIssuerSigningKey = true, // Validate signing key
                        IssuerSigningKey = key // Use the same key to validate the token
                    }, out var validatedToken);

                    // Attach the claims principal (user) to the HttpContext
                    context.User = claimsPrincipal;
                }
                catch
                {
                    // Token validation failed, return Unauthorized
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid or expired token.");
                    return;
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
