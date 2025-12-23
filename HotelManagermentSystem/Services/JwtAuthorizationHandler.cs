using System.Net.Http.Headers;

namespace HotelManagermentSystem.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<JwtAuthorizationHandler> _logger;

        public JwtAuthorizationHandler(ITokenService tokenService, ILogger<JwtAuthorizationHandler> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = _tokenService.Token;

            _logger.LogInformation(
                "[JWT HANDLER] Token null? {IsNull} | Length: {Length}",
                string.IsNullOrWhiteSpace(token),
                token?.Length ?? 0
            );

            if (!string.IsNullOrWhiteSpace(_tokenService.Token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _tokenService.Token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }

}
