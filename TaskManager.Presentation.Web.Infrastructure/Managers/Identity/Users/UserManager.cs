using TaskManager.Presentation.Web.Infrastructure.Extensions;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers.Identity.Users;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Results;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.User;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.User;
using TaskManager.Presentation.Web.Infrastructure.Results;
using TaskManager.Presentation.Web.Infrastructure.Routes.Identity.Account;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TaskManager.Presentation.Web.Infrastructure.Managers.Identity.Users
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;

        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> CreateUserAsync(CreateUserRequest request, string origin)
        {

            var response = await _httpClient.PostAsJsonAsync(AccountEndpoints.ADD(origin), request);
            return await response.ToResult<string>();
        }


        public async Task<IResult<EditUserRequest>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync(AccountEndpoints.GetById(id));
            return await response.ToResult<EditUserRequest>();
        }

        public async Task<IResult<string>> EditAsync(EditUserRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(AccountEndpoints.Edit, request);
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(AccountEndpoints.Delete(id));
            return await response.ToResult<int>();
        }

        public async Task<PaginatedResult<GetAllUsersResponse>> GetAllAsync(GetAllPagedUserRequest request)
        {
            var response = await _httpClient.GetAsync(
              AccountEndpoints.GetAllPaged(
                  request.PageNumber,
                  request.PageSize,
                  request.SearchString,
                  request.OrderBy)
              );
            return await response.ToPaginatedResult<GetAllUsersResponse>();
        }

  public async Task<PaginatedResult<GetAllUsersResponse>> GetAllConsumerAsync(GetAllPagedUserRequest request)
        {
            var response = await _httpClient.GetAsync(
              AccountEndpoints.GetAllConsumerAsync(
                  request.PageNumber,
                  request.PageSize,
                  request.SearchString,
                  request.OrderBy)
              );
            return await response.ToPaginatedResult<GetAllUsersResponse>();
        }


    }
}
