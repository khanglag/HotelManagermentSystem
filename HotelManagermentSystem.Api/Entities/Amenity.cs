using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;

        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
    }
}
