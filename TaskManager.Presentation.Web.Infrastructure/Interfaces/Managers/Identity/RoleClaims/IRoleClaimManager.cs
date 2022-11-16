using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers;

namespace TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimResponse role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}
