using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; } = 0;
        public int CheckinEmployeeId { get; set; }
        public int CheckoutEmployeeId { get; set; }
        public DateTime ActualCheckinTime { get; set; }
        public DateTime ActualCheckoutTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReservationStatus Status { get; set; }

        public Reservation() { }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;
        [ForeignKey("CheckinEmployeeId")]
        public Employee CheckinEmployee { get; set; }
        [ForeignKey("CheckoutEmployeeId")]
        public Employee CheckoutEmployee { get; set; }
        public Invoice? Invoice { get; set; }
        public ICollection<ReservationDetail>? ReservationDetails { get; set; }
    }
}
