using TaskManager.Infrastructure.Shared.Constants.IdentityConst;
using TaskManager.Infrastructure.Shared.Interfaces.Shared;
using TaskManager.Presentation.Web.Controllers.Admin;
using TaskManager.Presentation.Web.Controllers;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Authentication;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Users;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Token;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Presentation.Web.Controllers;

namespace TaskManager.Presentation.Web.Controllers.Admin
{

    /// <summary>
    /// Authentication controller. Responsible for functioning authentication related tasks 
    /// </summary>
    [Area(nameof(Identity))]
    [AllowAnonymous]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserManager _userManager;

        public AccountController(IAuthenticationManager authenticationManager, ICurrentUserService currentUser, IUserManager userManager)
        {
            _authenticationManager = authenticationManager;
            _currentUser = currentUser;
            _userManager = userManager;
        }

        #region >>> LogIn <<<
        /// <summary>
        /// Login Action method. Login page will be rendered to view from this action method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LogIn(string returnUrl = null)
        {
            if (_currentUser.IsAuthenticated() && (_currentUser.IsInRole(Roles.Manager) || _currentUser.IsInRole(Roles.Supervisor)))
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            else if (_currentUser.IsInRole(Roles.Employee))
            {
                return RedirectToAction("Search", "Home");
            }
            returnUrl ??= Url.Content("~/");
            return View();

        }

        /// <summary>
        /// Login Post Action method. This method will receive userName, password and will doe further process to complete login process
        /// </summary>
        /// <param name="loginUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(TokenRequest request, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationManager.Login(request);
                if (result.Succeeded)
                {

                    if (result.Data.Roles.Contains(Roles.Manager) || result.Data.Roles.Contains(Roles.Supervisor))
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                    else if (result.Data.Roles.Contains(Roles.Employee))
                    {
                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("Detail", "MerchantList");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
                //var rs = await _accountService.CookieSignInAsync(loginUserDto);
                //if (rs.Succeeded)
                //    return RedirectToAction("Index", "Dashboard", new { area = "Admin", succeeded = rs.Succeeded, message = rs.Message });
                //HttpContext.Session.GetString(SessionConstants.JWTName);
            }

            return View(request);
        }
        #endregion

        #region >>> Forgot Password <<<
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                string origin = GetOrigin();
                var result = await _authenticationManager.ForgotPassword(model, origin);
                if (result.Succeeded)
                {
                    return View("~/Areas/Identity/Views/Account/ForgotPasswordConfirmation.cshtml");

                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }
        #endregion

        #region >>> Sign Up <<<
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(CreateUserRequest model)
        {
            if (ModelState.IsValid)
            {
                model.EmailConfirmed = false;
                model.AccountType = Roles.Employee;
                string origin = GetOrigin();
                var savedDataResponse = await _userManager.CreateUserAsync(model, origin);
                if (savedDataResponse.Succeeded)
                {
                    return View("~/Areas/Identity/Views/Account/SignUpConfirmation.cshtml");
                }
                ModelState.AddModelError(string.Empty, savedDataResponse.Message);
            }
            return View(model);
        }
        #endregion


        #region >>> LogOut <<<
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            var isAdmin = (_currentUser.IsInRole(Roles.Manager) || _currentUser.IsInRole(Roles.Supervisor));
            var result = await _authenticationManager.Logout();
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                if (isAdmin)
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                return RedirectToAction("Index", "Home");
            }
            if (!isAdmin)
            {
                return RedirectToAction("Detail", "MerchantList");
            }
            return RedirectToAction(nameof(LogIn), "Account", new { Area = nameof(Identity) });
        }
        #endregion

        #region >>> Confirm Account <<<
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            var result = await _authenticationManager.ConfirmEmail(userId, code);
            if (!result.Succeeded)
            {
                return View("~/Areas/Identity/Views/Account/Error.cshtml");
            }
            return View();
        }
        #endregion

        #region >>> Reset Password<<<
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword([FromQuery] string token)
        {
            if (token == null)
            {
                // BadRequest - A code must be supplied for password reset.
                return View("~/Areas/Identity/Views/Account/Error.cshtml");
            }
            ResetPasswordRequest model = new() { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationManager.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    return View("~/Areas/Identity/Views/Account/ResetPasswordConfirmation.cshtml");
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }

            return View(model);
        }

        #endregion

    }
}
