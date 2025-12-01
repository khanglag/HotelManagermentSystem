namespace HotelManagementSystem.Api.Entities
{
    public class AccountPermission
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
