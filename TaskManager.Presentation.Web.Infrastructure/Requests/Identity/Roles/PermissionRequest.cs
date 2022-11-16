using System.Collections.Generic;

namespace TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Roles
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }
        public IList<RoleClaimRequest> RoleClaims { get; set; }
    }
}
