using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account
{
    public class RegisterRequest
    {
        public RegisterRequest()
        {
            IsActive = true;
            EmailConfirmed = false;
        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Marked as active user")]
        public bool IsActive { get; set; } = false;
        public bool EmailConfirmed { get; set; } = false;
    }
}
