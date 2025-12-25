namespace HotelManagermentSystem.View.Services
{
    public interface ITokenService
    {
        string? Token { get; set; }
        string RefreshToken { get; set; }
    }

}
