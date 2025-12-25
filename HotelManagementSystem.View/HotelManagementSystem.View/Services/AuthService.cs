using HotelManagermentSystem.View.Models.Requests;
using HotelManagermentSystem.View.Models.Responses;
using Microsoft.JSInterop;

namespace HotelManagermentSystem.View.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            HttpClient httpClient,
            IJSRuntime jsRuntime,
            ITokenService tokenService,
            ILogger<AuthService> logger)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<LoginResponse?> Login(LoginDto loginDto)
        {
            var response = await _httpClient
                .PostAsJsonAsync("api/Account/login", loginDto);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "[LOGIN] Failed - StatusCode: {StatusCode}",
                    response.StatusCode);

                return null;
            }

            var loginResponse = await response.Content
            .ReadFromJsonAsync<LoginResponse>();

            if (loginResponse is null)
            {
                _logger.LogWarning("[LOGIN] Response body is NULL");
                return null;
            }

            // ✅ LOG token từ API
            _logger.LogInformation(
                "[LOGIN] API Token received | Length: {Length}",
                loginResponse.Token?.Length ?? 0);

            _logger.LogInformation(
                "[LOGIN] API RefreshToken received | Length: {Length}",
                loginResponse.RefreshToken?.Length ?? 0);

            // ⚠️ KHÔNG log full token ở production
            _logger.LogDebug(
                "[LOGIN] Token Preview: {Preview}",
                loginResponse.Token?.Substring(0, Math.Min(20, loginResponse.Token.Length)));

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "authToken",
                loginResponse.Token);

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "refreshToken",
                loginResponse.RefreshToken);

            _tokenService.Token = loginResponse.Token;
            _tokenService.RefreshToken = loginResponse.RefreshToken;
            // Kiểm tra ngay lập tức
            if (!string.IsNullOrEmpty(_tokenService.Token))
            {
                var preview = _tokenService.Token.Substring(0, 10);
                _logger.LogInformation("[CHECK] Token đã lưu vào Service. Bắt đầu bằng: {Preview}...", preview);
            }
            else
            {
                _logger.LogWarning("[CHECK] Lỗi: Token vẫn đang bị NULL!");
            }

            return loginResponse;
        }

        public async Task Logout()
        {
            // để trống – logout xử lý ở UI
            await Task.CompletedTask;
        }
    }
}
