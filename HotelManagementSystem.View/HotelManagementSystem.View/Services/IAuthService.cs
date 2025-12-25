using HotelManagermentSystem.View.Models.Requests;
using HotelManagermentSystem.View.Models.Responses;

namespace HotelManagermentSystem.View.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> Login(LoginDto loginDto);
        Task Logout();
    }
}
