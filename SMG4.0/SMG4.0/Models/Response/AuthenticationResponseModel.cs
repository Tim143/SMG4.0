namespace SMG4._0.Models.Response
{
    public class AuthenticationResponseModel
    {
        public long Id { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Errors { get; set; }
    }
}
