using HotelManagermentSystem.View.Models.Enum;

namespace HotelManagermentSystem.View.Models.Requests
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BranchId { get; set; }
        public int AccountId { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}
