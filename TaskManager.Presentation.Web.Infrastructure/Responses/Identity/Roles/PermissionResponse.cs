using System.Collections.Generic;

namespace TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Roles
{
    public class PermissionResponse
    {
        public PermissionResponse()
        {
            RoleClaims = new();
        }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaimResponse> RoleClaims { get; set; }
    }
}
