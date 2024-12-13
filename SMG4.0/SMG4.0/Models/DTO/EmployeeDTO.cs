namespace SMG4._0.Models.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public DateOnly EmploymentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
