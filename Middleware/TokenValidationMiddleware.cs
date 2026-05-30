using Project_5_final.Models;
using Project_5_final.Services.Implementation;
using Project_5_final.Services.Interface;

namespace Project_5_final.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider invalidTokenService)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var userService =invalidTokenService.GetRequiredService<IUserService>();
                var isValid = await userService.IsTokenValidAsync(token);
                if (!isValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is invalidated");
                    return;
                }
            }

            await _next(context);
        }
        
    }
}
