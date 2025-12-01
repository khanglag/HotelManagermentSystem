using HotelManagementSystem.Api.Repositories;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using HotelManagementSystem.Api.Services;
using HotelManagementSystem.Api.Services.IService;
using HotelManagementSystem.Api.Services.IServices;

namespace HotelManagementSystem.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<PermissionService>();
            return services;
        }
    }
}
