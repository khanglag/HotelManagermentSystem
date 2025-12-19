using HotelManagementSystem.Api.Enums;

namespace HotelManagermentSystem.Api.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; } // Thêm để hiển thị
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CheckinEmployeeId { get; set; }
        public int? CheckoutEmployeeId { get; set; }
        public DateTime? ActualCheckinTime { get; set; }
        public DateTime? ActualCheckoutTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReservationStatus Status { get; set; }
    }
}