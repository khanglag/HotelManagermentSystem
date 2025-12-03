using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(250)]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
        [StringLength(250)]
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        [Required]
        public AccountStatus Status { get; set; }

        public ICollection<AccountPermission>? AccountPermissions { get; set; }

        public Account() 
        {
        }
    }
}
