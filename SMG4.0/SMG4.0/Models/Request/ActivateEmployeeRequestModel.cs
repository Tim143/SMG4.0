namespace SMG4._0.Models.Request
{
    public class ActivateEmployeeRequestModel
    {
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}
