using System.Collections.Generic;
using System.Security.Claims;

namespace TaskManager.Infrastructure.Shared.Interfaces.Shared
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserName { get; }
        string FirstName { get; }
        string LastName { get; set; }
        string FullName { get; set; }
        string IpAddress { get; set; }
        string Jti { get; set; }
        string Email { get; set; }
        IEnumerable<Claim> Roles { get; }

        bool IsInRole(string role);
        public bool IsAuthenticated();


    }
}
