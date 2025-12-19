using Blazored.LocalStorage;
using HotelManagermentSystem.Models;

namespace HotelManagermentSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginDto);

            Console.WriteLine($"StatusCode: {response.StatusCode}");

            var raw = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw response: {raw}");

            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result != null)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
        }
    }
}
