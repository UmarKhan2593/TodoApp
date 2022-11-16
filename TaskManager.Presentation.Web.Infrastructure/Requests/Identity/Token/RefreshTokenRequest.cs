namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Token
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
