using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.Responses.Identity.User
{
    public class GetAllUsersResponse
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Status { get; set; }

        //public string Role { get; set; }

        public string Id { get; set; }
    }
}
