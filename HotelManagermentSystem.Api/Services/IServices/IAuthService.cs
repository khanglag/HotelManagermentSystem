using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Services.IService
{
    public interface IAuthService
    {
        string GenerateJwtToken(Account account);
        string GenerateRefreshToken();
        Task<Account?> RefreshTokenAsync(string refreshToken, string userName);
    }
}
