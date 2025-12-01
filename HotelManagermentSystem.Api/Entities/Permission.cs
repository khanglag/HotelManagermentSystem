using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Path { get; set; }
        [Required]
        [StringLength(10)]
        public string Method { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        public ICollection<AccountPermission>? AccountPermissions { get; set; }

        public Permission()
        {
            //AccountPermissions = new HashSet<AccountPermission>();
        }
    }
}
