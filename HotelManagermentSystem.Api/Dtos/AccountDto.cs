using HotelManagementSystem.Api.Enums;

namespace HotelManagementSystem.Api.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public Role Role { get; set; } 
        public string Exist { get; set; } = "";
    }
}
