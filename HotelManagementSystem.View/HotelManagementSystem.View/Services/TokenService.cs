namespace HotelManagermentSystem.View.Services
{
    public class TokenService : ITokenService
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }

}
