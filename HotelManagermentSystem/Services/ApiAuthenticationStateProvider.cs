using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace HotelManagermentSystem.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly ITokenService _tokenService;
        public ApiAuthenticationStateProvider(
            IJSRuntime jsRuntime,
            ITokenService tokenService)
        {
            _jsRuntime = jsRuntime;
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(_anonymous);

                _tokenService.Token = token;

                var claims = JwtParser.ParseClaimsFromJwt(token);

                // Chỉ định rõ: "sub" là Name và "role" là Role
                var identity = new ClaimsIdentity(
                     claims,
                     "jwt",
                     ClaimTypes.Name,
                     ClaimTypes.Role
                 );
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
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
