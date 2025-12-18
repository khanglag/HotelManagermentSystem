namespace HotelManagermentSystem.Api.Dtos
{
    public class ServiceUsageDto
    {
        public int Id { get; set; }
        public int ReservationDetailId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime UsedAt { get; set; }

        // Thêm tên dịch vụ để hiển thị ở Frontend dễ hơn
        public string? ServiceName { get; set; }
    }
}