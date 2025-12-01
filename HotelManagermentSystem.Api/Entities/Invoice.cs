using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public DateTime IssuedAt { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public InvoiceStatus Status { get; set; }
        public Invoice() { }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; } = null!;

        public ICollection<Payment>? Payments { get; set; }
    }
}
