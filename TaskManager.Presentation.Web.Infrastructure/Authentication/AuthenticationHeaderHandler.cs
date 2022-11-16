using TaskManager.Infrastructure.Shared.Constants.IdentityConst;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor? _contextAccessor;

        public AuthenticationHeaderHandler(IHttpContextAccessor? contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(
          HttpRequestMessage request,
          CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != CustomJwtConstant.AuthenticationScheme)
            {
                var savedToken = _contextAccessor.HttpContext.Session.GetString(Constants.Sessions.SessionConstants.AuthToken);

                if (!string.IsNullOrWhiteSpace(savedToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(CustomJwtConstant.HeaderValueName, savedToken);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }

        #region Set principal method.

        /// <summary>
        /// Set principal method.
        /// </summary>
        /// <param name="principal">Principal parameter</param>
        //private static void SetPrincipal(IPrincipal principal)
        //{
        //    // setting.
        //    Thread.CurrentPrincipal = principal;
        //    // Verification.
        //    if (HttpContext.Current != null)
        //    {
        //        // Setting.
        //        HttpContext.Current.User = principal;
        //    }
        //}

        #endregion

    }
}
