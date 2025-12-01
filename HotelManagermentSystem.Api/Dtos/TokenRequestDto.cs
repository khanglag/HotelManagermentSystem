namespace HotelManagementSystem.Api.Dtos
{
    public class TokenRequestDto
    {
        public string UserName { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
