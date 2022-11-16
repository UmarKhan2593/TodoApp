using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Token;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Token;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Authentication
{
    public interface IAuthenticationManager : IManager
    {
        Task<IResult<TokenResponse>> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<IResult> ForgotPassword(ForgotPasswordRequest model,string origin);

        Task<IResult> ConfirmEmail(string userId, string code);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
