using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
        public ICollection<Reservation>? Reservations { get; set; } = new HashSet<Reservation>();
    }
}
