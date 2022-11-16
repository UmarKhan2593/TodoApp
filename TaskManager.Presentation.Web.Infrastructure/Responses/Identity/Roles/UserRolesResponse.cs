using TaskManager.Presentation.Web.Infrastructure.CommonDTOs;
using System.Collections.Generic;

namespace TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Roles
{
    public class UserRolesResponse
    {
        public UserRolesResponse()
        {
            UserRoles = new();
        }
        public string UserId { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }





}
