using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account;

namespace TaskManager.API.Controllers.Identity
{
    /// <summary>
    /// Controller for Authentication and Authorization
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        #region >>> Properties <<<
        private readonly IAuthenticationService _userService;
        #endregion


        #region >>> Constructor <<<
        /// <summary>
        /// Inject identity service to identity controller 
        /// </summary>
        /// <param name="userService"></param>
        public AuthenticationController(IAuthenticationService userService)
        {
            this._userService = userService;
        }
        #endregion


        #region >>> Action Method <<<

        /// <summary>
        /// Generates a JSON Web Token for a valid combination of emailId and password.
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest tokenRequest)
        {
            var ipAddress = GetIPAddress();
            var token = await _userService.GetTokenAsync(tokenRequest, ipAddress);
            return Ok(token);
        }

        /// <summary>
        /// Register a user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _userService.RegisterAsync(request, origin));
        }

        /// <summary>
        /// Confirm email 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns>Status 200 OK</returns>
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _userService.ConfirmEmailAsync(userId, code));
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="model"></param>
        /// <param name="origin"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            return Ok(await _userService.ForgotPasswordAsync(model, origin));
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _userService.ResetPasswordAsync(model));
        }

        #endregion

        /// <summary>
        /// Toggle User Status (Activate and Deactivate)
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("toggle-status")]
        public async Task<IActionResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            return Ok(await _userService.ToggleUserStatusAsync(request));
        }


        #region >>> General NON-Actions <<<
        [NonAction]
        private string GetIPAddress()
        {
            // X-Forwarded-For (XFF) header is a de-facto standard header for identifying the originating IP address of a
            // client connecting to a web server through an HTTP proxy or a load balancer
            // X-Forwarded-For: <client>, <proxy1>, <proxy2>
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        #endregion



    }
}
