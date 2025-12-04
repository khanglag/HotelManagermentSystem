using HotelManagementSystem.Api.Repositories;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using HotelManagementSystem.Api.Services;
using HotelManagementSystem.Api.Services.IService;
using HotelManagementSystem.Api.Services.IServices;
using HotelManagermentSystem.Api.Repositories;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services;
using HotelManagermentSystem.Api.Services.IServices;

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
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<PermissionService>();
            return services;
        }
    }
}
