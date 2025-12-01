namespace HotelManagementSystem.Api.Enums
{
    public enum RoomStatus
    {
        VACANT_CLEAN, // Room is available and clean
        VACANT_DIRTY, // Room is available but needs cleaning
        OCCUPIED, // Room is currently occupied by a guest
        UNDER_MAINTENANCE, // Room is under maintenance and not available for booking
        OUT_OF_SERVICE // Room is out of service and cannot be booked
    }
}
