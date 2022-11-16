using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Token
{
    public class TokenRequest
    {
        [Required]
        [Display(Name ="Email / User Name")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
