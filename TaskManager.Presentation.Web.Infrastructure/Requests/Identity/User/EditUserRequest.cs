
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.User
{
    public class EditUserRequest
    {

        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Logo { get; set; }
    }
}
