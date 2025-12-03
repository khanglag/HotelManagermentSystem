using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HotelManagementSystem.Api.Services
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, PermissionService permissionService)
        {
            Console.WriteLine("=================");
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                string authHeader = context.Request.Headers["Authorization"].ToString();
                Console.WriteLine($"[HEADER DEBUG] Authorization Header: {authHeader}");
            }
            else
            {
                Console.WriteLine("[HEADER DEBUG] Authorization Header: NOT FOUND");
            }
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }
            var requiredPermission = endpoint.Metadata.GetMetadata<RequiredPermissionAttribute>();

            if (requiredPermission == null)
            {
                await _next(context);
                return;
            }
            
            var userPerms = context.User.Claims
                .Where(c => c.Type == "perm")
                .Select(c => c.Value)
                .ToList();

            Console.WriteLine("UserPerms: " + string.Join(", ", userPerms));
            Console.WriteLine("RequiredPerm: " + requiredPermission.PermissionKey);
            if (!userPerms.Any(p => p.Equals(requiredPermission.PermissionKey, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("UserPerms: " + string.Join(", ", userPerms));
                Console.WriteLine("RequiredPerm: " + requiredPermission.PermissionKey);

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: You don't have permission to access this resource.");
                return;
            }

            await _next(context);
        }
    }

}
