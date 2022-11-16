using Microsoft.AspNetCore.Mvc.Rendering;


namespace TaskManager.Presentation.Web.Extensions
{
    public static class Utilities
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string control)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            //var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control == routeControl;

            return returnActive ? "active" : "";
        }

    }
}
