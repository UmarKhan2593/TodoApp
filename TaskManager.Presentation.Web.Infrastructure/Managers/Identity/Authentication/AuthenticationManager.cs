using TaskManager.Infrastructure.Shared.Constants.IdentityConst;
using TaskManager.Presentation.Web.Infrastructure.Constants.Sessions;
using TaskManager.Presentation.Web.Infrastructure.Extensions;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Authentication;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Managers.Base;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Token;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Token;
using TaskManager.Presentation.Web.Infrastructure.Results;
using TaskManager.Presentation.Web.Infrastructure.Routes.Identity.Authentication;
using TaskManager.Presentation.Web.Infrastructure.Routes.Identity.Tokens;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Managers.Identity.Authentication
{
    public class AuthenticationManager : BaseManager<AuthenticationManager>, IAuthenticationManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthenticationManager(
            IHttpContextAccessor contextAccessor,
            HttpClient httpClient) : base(httpClient)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<IResult<TokenResponse>> Login(TokenRequest model)
        {
            var response = await httpClient.PostAsJsonAsync(TokenEndpoints.Get, model);
            var result = await response.ToResult<TokenResponse>();
            if (result.Succeeded)
            {
                var token = result.Data.JWToken;
                var refreshToken = result.Data.RefreshToken ?? "";
                //_contextAccessor.HttpContext.Session.SetString(SessionConstants.AuthToken, token);
                //_contextAccessor.HttpContext.Session.SetString(SessionConstants.RefreshToken, refreshToken);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CustomJwtConstant.AuthenticationScheme, token);
                return await Result<TokenResponse>.SuccessAsync(result.Data);
            }
            else
            {
                return await Result<TokenResponse>.FailAsync(result.Message);
            }
        }




        public async Task<IResult> Logout()
        {
            _contextAccessor.HttpContext.Session.Remove(SessionConstants.AuthToken);
            _contextAccessor.HttpContext.Session.Remove(SessionConstants.RefreshToken);
            httpClient.DefaultRequestHeaders.Authorization = null;
            //_contextAccessor.HttpContext.Session.Remove(SessionConstants.UserImageURL);
            //((ApplicationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            return await Result.SuccessAsync();

        }

      

       

        public async Task<IResult> ForgotPassword(ForgotPasswordRequest model,string origin)
        {
            var response = await httpClient.PostAsJsonAsync(AuthenticationEndpoints.ForgetPassword(origin), model);
            var result = await response.ToResult<TokenResponse>();
            if (result.Succeeded)
            {
                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailAsync(result.Message);
            }
        }

        public async Task<IResult> ConfirmEmail(string userId, string code)
        {
            var response = await httpClient.GetAsync(AuthenticationEndpoints.ConfirmEmail(userId,code));
            var result = await response.ToResult<string>();
            if (result.Succeeded)
            {
                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailAsync(result.Message);
            }
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var response = await httpClient.PostAsJsonAsync(AuthenticationEndpoints.ResetPassword, request);
            return await response.ToResult();
        }


    }
}
