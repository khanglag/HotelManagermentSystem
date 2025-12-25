using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace HotelManagermentSystem.View.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly ITokenService _tokenService;

        private readonly ILogger<ApiAuthenticationStateProvider> _logger;

        public ApiAuthenticationStateProvider(
             IJSRuntime jsRuntime,
             ITokenService tokenService,
             ILogger<ApiAuthenticationStateProvider> logger) // Sửa dấu ngoặc tại đây
        {
            _jsRuntime = jsRuntime;
            _tokenService = tokenService;
            _logger = logger;
        }

        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    try
        //    {
        //        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        //        if (string.IsNullOrWhiteSpace(token))
        //            return new AuthenticationState(_anonymous);

        //        _tokenService.Token = token;

        //        var claims = JwtParser.ParseClaimsFromJwt(token);

        //        // Chỉ định rõ: "sub" là Name và "role" là Role
        //        var identity = new ClaimsIdentity(
        //             claims,
        //             "jwt",
        //             ClaimTypes.Name,
        //             ClaimTypes.Role
        //         );
        //        var user = new ClaimsPrincipal(identity);

        //        return new AuthenticationState(user);
        //    }
        //    catch
        //    {
        //        return new AuthenticationState(_anonymous);
        //    }
        //}
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _logger.LogInformation("[Log]: GetAuthenticationStateAsync");
            try
            {
                // Kiểm tra xem Service đã có sẵn Token chưa (đã được nạp từ Layout chưa)
                var token = _tokenService.Token;

                // Nếu Service chưa có, lúc này mới thử đọc từ Browser
                if (string.IsNullOrEmpty(token))
                {
                    // Thêm try-catch nhỏ cho JS để tránh crash app khi Prerender
                    try
                    {
                        token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                    }
                    catch { return new AuthenticationState(_anonymous); }
                }

                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(_anonymous);

                // Nạp vào service nếu lấy được từ JS
                _tokenService.Token = token;

                var claims = JwtParser.ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xác thực");
                return new AuthenticationState(_anonymous);
            }
        }

        // Hàm này gọi sau khi login thành công để báo cho UI cập nhật
        public void MarkUserAsAuthenticated(string token)
        {
            _tokenService.Token = token;

            var claims = JwtParser.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(
                claims,
                "jwt",
                ClaimTypes.Name,
                ClaimTypes.Role
            );
            var user = new ClaimsPrincipal(identity);
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            _tokenService.Token = null;
            var authState = Task.FromResult(new AuthenticationState(_anonymous));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
