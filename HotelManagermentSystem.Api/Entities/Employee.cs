using HotelManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Api.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public int BranchId { get; set; }
        public EmployeeStatus Status { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; } = null!;
        [Required]
        public int AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; } = null!;
    }
}
