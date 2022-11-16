
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Infrastructure.Shared.Constants.IdentityConst;
using TaskManager.Infrastructure.Shared.Interfaces.Shared;

namespace TaskManager.Presentation.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        #region >>> Additional Properties <<<
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Get first name of current user 
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Get last name of current user 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get full name of current user 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Get Ip Address of current user 
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Get Jti of current user 
        /// </summary>
        public string Jti { get; set; }

        /// <summary>
        /// Get email of current user 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Get List of Roles of current user 
        /// </summary>
        public IEnumerable<Claim> Roles { get; }

        /// <summary>
        /// Check role for current user 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role) => _httpContextAccessor.HttpContext.User.IsInRole(role);

        /// <summary>
        /// Check user is authenticated or not 
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated() => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        #endregion

        #region >>> IAuthenticatedUserService => Implementation for basic properties <<<

        /// <summary>
        /// Authenticated User Id
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Authenticated User Name
        /// </summary>
        public string UserName { get; }

        #endregion

        #region >>> Constructor <<<
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimNamesConstants.UserId)?.Value;
            UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            FirstName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimNamesConstants.FirstName)?.Value;
            LastName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimNamesConstants.LastName)?.Value;
            FullName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimNamesConstants.FullName)?.Value;
            IpAddress = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimNamesConstants.IpAddress)?.Value;
            Jti = httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            Email = httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role);
        }



        #endregion
    }
}
