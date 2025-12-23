using HotelManagermentSystem.Models.Requests;
using HotelManagermentSystem.Models.Responses;

namespace HotelManagermentSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse?> Login(LoginDto loginDto)
        {
            var response = await _httpClient
                .PostAsJsonAsync("api/Account/login", loginDto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<LoginResponse>();
        }

        public async Task Logout()
        {
            // để trống – logout xử lý ở UI
            await Task.CompletedTask;
        }
    }
}
