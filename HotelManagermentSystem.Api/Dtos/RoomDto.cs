using HotelManagementSystem.Api.Enums;

namespace HotelManagermentSystem.Api.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public int BranchId { get; set; }
        public RoomStatus Status { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
