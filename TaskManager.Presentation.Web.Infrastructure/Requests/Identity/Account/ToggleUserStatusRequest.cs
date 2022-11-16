namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account
{
    public class ToggleUserStatusRequest
    {
        public bool ActivateUser { get; set; }
        public string UserId { get; set; }
    }
}
