namespace HotelManagermentSystem.Api.Dtos
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}