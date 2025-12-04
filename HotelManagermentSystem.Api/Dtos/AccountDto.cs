using HotelManagementSystem.Api.Enums;

namespace HotelManagementSystem.Api.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; }
        public Role Role { get; set; } 
        public AccountStatus Status { get; set; }
    }
}
