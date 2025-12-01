using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Entities
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public Service() { }

        public ICollection<ServiceUsage>? ServiceUsages { get; set; }
    }
}
