
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.User
{
    public class CreateUserRequest
    {
        public CreateUserRequest()
        {
            IsActive = true;
            EmailConfirmed = true;
        }

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

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Marked as active user")]
        public bool IsActive { get; set; }

        public string Logo { get; set; }

        [Display(Name = "Auto Confirm Email?")]
        public bool EmailConfirmed { get; set; }

        public string AccountType { get; set; }

    }
}
