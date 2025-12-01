using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; }
        [Required]
        [ForeignKey(nameof(RoomType))]
        public int RoomTypeId { get; set; }
        [Required]
        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }
        [Required]
        public RoomStatus Status { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Branch Branch { get; set; } = null;
        public RoomType RoomType { get; set; } = null;

        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
        public ICollection<ReservationDetail>? ReservationDetails { get; set; }

    }
}
