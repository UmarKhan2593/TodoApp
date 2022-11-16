using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Account;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Account
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

        Task<IResult<string>> GetProfilePictureAsync(string userId);
    }
}
