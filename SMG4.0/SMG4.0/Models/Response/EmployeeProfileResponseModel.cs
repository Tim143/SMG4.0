namespace SMG4._0.Models.Response
{
    public class EmployeeProfileResponseModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public DateOnly EmploymentDate { get; set; }
    }
}
