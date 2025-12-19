using HotelManagementSystem.Api.Enums;

namespace HotelManagermentSystem.Api.Dtos
{
    public class ReservationDetailDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; } // Hiển thị số phòng
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public bool IsHourlyRate { get; set; }
        public decimal AgreedHourlyRate { get; set; }
        public decimal AgreedBaseDailyPrice { get; set; }
        public decimal AgreedExtraHourRate { get; set; }
        public decimal SubTotalPrice { get; set; }
        public ReservationDetailStatus Status { get; set; }
    }
}