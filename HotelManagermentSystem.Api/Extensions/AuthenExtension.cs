using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace HotelManagementSystem.Api.Extensions
{
    public static class AuthenExtention
    {
        public static void AddAuthenExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("AUTH FAILED: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    // 1. Chỉ định Claim nào trong JWT là Role
                    // Giả định bạn đang sử dụng ClaimTypes.Role,
                    // thường được ánh xạ tới "role" trong JWT.
                    RoleClaimType = ClaimTypes.Role,

                    // 2. (Khuyến nghị) Chỉ định Claim nào là Name/Username
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
        }
    }
}
