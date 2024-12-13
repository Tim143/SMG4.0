namespace SMG4._0.Models.Response
{
    public class AuthenticationResponseModel
    {
        public int Id { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
