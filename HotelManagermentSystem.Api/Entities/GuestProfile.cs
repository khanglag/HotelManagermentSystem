using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class GuestProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; } 
        public DateOnly DayOfBirth { get; set; }
        public string INumber { get; set; }
        [Required]
        public int ReservationDetailId { get; set; }

        [ForeignKey(nameof(ReservationDetailId))]
        public ReservationDetail ReservationDetail { get; set; } = null!;
    }
}
