namespace TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Account
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        // TODO:- Configure - Either the new user is active or not will be configured here Web level
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureDataUrl { get; set; }
    }
}
