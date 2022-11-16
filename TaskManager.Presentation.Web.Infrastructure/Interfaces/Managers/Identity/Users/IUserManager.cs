using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.User;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Account;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.User;
using TaskManager.Presentation.Web.Infrastructure.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Users
{
    public interface IUserManager : IManager
    {
        Task<PaginatedResult<GetAllUsersResponse>> GetAllAsync(GetAllPagedUserRequest request);
        Task<IResult> CreateUserAsync(CreateUserRequest request, string origin );

        Task<IResult<EditUserRequest>> GetByIdAsync(string id);

        Task<IResult<string>> EditAsync(EditUserRequest request);

        Task<IResult<int>> DeleteAsync(string id);
        Task<PaginatedResult<GetAllUsersResponse>> GetAllConsumerAsync(GetAllPagedUserRequest request);
        //Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request);
        //Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
        //Task<IResult<UserResponse>> GetAsync(string userId);
        //Task<IResult<UserRolesResponse>> GetRolesAsync(string userId);
        //Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);
        //Task<IResult> UpdateRolesAsync(UserRolesResponse request);

    }
}
