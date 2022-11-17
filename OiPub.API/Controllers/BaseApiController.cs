using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TaskManager.API.Controllers.Base
{
    /// <summary>
    /// Base Api Controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase
    {
        #region >>> Properties <<<

        private ILogger<T> _loggerInstance;

        /// <summary>
        /// Logger Instance
        /// </summary>
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();


        #endregion
    }
}
