using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Entities
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Location { get; set; }

        public Branch() { }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
