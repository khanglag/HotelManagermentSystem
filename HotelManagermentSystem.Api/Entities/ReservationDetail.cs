using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class ReservationDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime CheckInDateTime { get; set; }
        [Required]
        public DateTime CheckOutDateTime { get; set; }
        [Required]
        public bool IsHourlyRate { get; set; }
        public decimal AgreedHourlyRate { get; set; }
        public decimal AgreedBaseDailyPrice { get; set; }
        public decimal AgreedExtraHourRate { get; set; }

        public decimal SubTotalPrice { get; set; }
        public ReservationDetailStatus Status { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public Room Room { get; set; } = null!;

        public ReservationDetail() { }

        public ICollection<GuestProfile>? GuestProfiles { get; set; }
        public ICollection<ReservationDetail>? ReservationDetails { get; set; }
        public ICollection<ServiceUsage>? ServiceUsages { get; set; }

    }
}
