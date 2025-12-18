namespace HotelManagermentSystem.Api.Dtos
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal DailyPrice { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal ExtraHourPrice { get; set; }
        public int MaxHourlyDuration { get; set; }
    }
}
