using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Presentation.Web.Controllers
{

    /// <summary>
    /// Base Controller
    /// </summary>
    public abstract class BaseController<T> : Controller
    {
        private ILogger<T> _loggerInstance;
        private IMapper _mapperInstance;

        protected ILogger<T> Logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();



        #region >>>  Non Action/Helper <<<
        #region >>> GetOrigin <<<
        [NonAction]
        protected string GetOrigin() => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        #endregion

        #region >>> Set Back URL Path Methods <<<
        [NonAction]
        protected void SetBackUrl(string action)
        {
            ViewBag.BackPath = $"{action}";
        }

        [NonAction]
        protected void SetBackUrl(string controller, string action)
        {
            ViewBag.BackPath = $"/{controller}/{action}";
        }

        [NonAction]
        protected void SetBackUrl(string controller, string action, string routeData)
        {
            ViewBag.BackPath = $"/{controller}/{action}/{routeData}";
        }

        [NonAction]
        protected void SetBackUrl(string controller, string action, string routeData, string addtionalUrlParameter)
        {
            ViewBag.BackPath = $"/{controller}/{action}/{routeData}?{addtionalUrlParameter}";
        }

        [NonAction]
        protected void SetBackUrlWithArea(string area, string controller, string action)
        {
            ViewBag.BackPath = $"/{area}/{controller}/{action}";
        }

        [NonAction]
        protected void SetBackUrlWithArea(string area, string controller, string action, string routeData)
        {
            ViewBag.BackPath = $"/{area}/{controller}/{action}/{routeData}";
        }


        [NonAction]
        protected void SetBackUrlWithArea(string area, string controller, string action, string routeData, string addtionalUrlParameter)
        {
            ViewBag.BackPath = $"/{area}/{controller}/{action}/{routeData}?{addtionalUrlParameter}";
        }

        #endregion

        #region >>> Gard On Default Value of pull down menu
        protected bool GardOnNegativeValueOfPulldown(string propertyName, int? propertyValue, string propertyDispalyName)
        {
            if (propertyValue <= 0)
            {
                ModelState.AddModelError(propertyName, $"{propertyDispalyName} is required.");
                return false;
            }
            return true;
        }
        #endregion

        #endregion

    }
}
