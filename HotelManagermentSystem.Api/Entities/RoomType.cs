using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Entities
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public decimal DailyPrice { get; set; }
        [Required]
        public decimal HourlyPrice { get; set; }
        [Required]
        public decimal ExtraHourPrice { get; set; }
        [Required]
        public int MaxHourlyDuration { get; set; }
        public RoomType () { }
        public ICollection<Room>? Rooms { get; set; }
        }
}
