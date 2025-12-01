using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class ServiceUsage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReservationDetailId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime UsedAt { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public ServiceUsage() { }
        [ForeignKey("ReservationDetailId")]
        public ReservationDetail ReservationDetail { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}
