using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Roles;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Roles
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleResponse role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}
