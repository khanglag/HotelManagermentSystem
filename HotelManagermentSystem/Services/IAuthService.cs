using HotelManagermentSystem.Models;

namespace HotelManagermentSystem.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginDto loginDto);
        Task Logout();
    }
}
