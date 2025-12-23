using HotelManagermentSystem.Models.Requests;
using HotelManagermentSystem.Models.Responses;

namespace HotelManagermentSystem.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> Login(LoginDto loginDto);
        Task Logout();
    }
}
