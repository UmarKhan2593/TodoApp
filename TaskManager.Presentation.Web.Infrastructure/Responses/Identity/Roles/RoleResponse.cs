using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Roles
{
    public class RoleResponse
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
