using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int InvoiceId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public PaymentMethod Method { get; set; }
        [Required]
        public PaymentStatus Status { get; set; }
        public Payment() { }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
    }
}
