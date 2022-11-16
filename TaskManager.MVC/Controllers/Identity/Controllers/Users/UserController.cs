using TaskManager.Infrastructure.Shared.Constants.IdentityConst;
using TaskManager.Presentation.Web.Controllers;
using TaskManager.Presentation.Web.Infrastructure.Abstraction;
using TaskManager.Presentation.Web.Infrastructure.Constants;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Users;
using TaskManager.Presentation.Web.Infrastructure.Interfaces;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Controllers.Admin.Identity.Users
{
    [Area(nameof(Identity))]
    [Authorize(Policy =CustomPolicyNames.Admin)]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        #region >>> Get All Users <<< 
        public IActionResult Index()
        {
            SetBackUrl("AdminDashboard", "Index");
            return View();
        }

        [HttpPost("User/GetReportData")]
        public async Task<JsonResult> GetReportData()
        {
            GetAllPagedUserRequest request = new()
            {
                //PageSize = pagedData.PageSize,
                //PageNumber = pagedData.PageNumber,
                //SearchString = pagedData.SearchString,
                //OrderBy = pagedData.OrderBy
            };

            var response = await _userManager.GetAllAsync(request);
            return Json(response);
        }

        #endregion

        #region >>> Create <<<
        //[HttpGet("User/Create")]
        public ActionResult Create()
        {
            CreateUserRequest model = new();
            SetBackUrlWithArea(nameof(Identity), "User", "Index");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserRequest model)
        {
            if (ModelState.IsValid)
            {
                model.AccountType = Roles.Admin;
                string origin = GetOrigin();
                var savedDataResponse = await _userManager.CreateUserAsync(model, origin);
                if (savedDataResponse.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, savedDataResponse.Message);
            }
            SetBackUrlWithArea(nameof(Identity), "User", "Index");
            return View(model);
        }
        #endregion


        #region >>> Edit <<<
        public async Task<ActionResult> Edit(string id, string error = "")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            var result = await _userManager.GetByIdAsync(id);
            if (result.Data == null)
            {
                return NotFound();
            }
            SetBackUrlWithArea(nameof(Identity), "User", "Index");
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, EditUserRequest model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var savedDataResponse = await _userManager.EditAsync(model);
                if (savedDataResponse.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, savedDataResponse.Message);
            }
            SetBackUrlWithArea(nameof(Identity), "User", "Index");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            var savedDataResponse = await _userManager.DeleteAsync(id);
            if (savedDataResponse.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Edit), new { id = id, error = savedDataResponse.Message });
        }
        #endregion


    }
}
