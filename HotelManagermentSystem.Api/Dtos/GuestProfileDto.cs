namespace HotelManagermentSystem.Api.Dtos
{
    public class GuestProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DayOfBirth { get; set; }
        public string IdNumber { get; set; }
        public int ReservationDetailId { get; set; }
    }
}
