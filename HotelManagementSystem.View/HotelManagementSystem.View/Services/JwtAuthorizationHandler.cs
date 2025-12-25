using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace HotelManagermentSystem.View.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<JwtAuthorizationHandler> _logger;

        public JwtAuthorizationHandler(ITokenService tokenService, IJSRuntime jSRuntime, ILogger<JwtAuthorizationHandler> logger)
        {
            _tokenService = tokenService;
            _jsRuntime = jSRuntime;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Lấy token từ Service, nếu null thì lấy từ LocalStorage
            var token = _tokenService.Token;
            _logger.LogInformation("[Log]: 1" + token);

            if (string.IsNullOrEmpty(token))
            {
                try
                {
                    // Bước này cực kỳ quan trọng để "cứu" các request lúc vừa load trang
                    token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                    _tokenService.Token = token; // Nạp ngược lại vào Service cho các lần sau
                }
                catch (Exception ex)
                {
                    _logger.LogError("[JWT HANDLER] JS Error: {Message}", ex.Message);
                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            _logger.LogInformation("[JWT HANDLER] Token Status: {status}", string.IsNullOrEmpty(token) ? "MISSING" : "ATTACHED");
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
