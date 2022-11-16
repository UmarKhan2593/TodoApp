using Application.DTOs.Generic;
using Application.DTOs.Response;
using Application.Interfaces.Repositories.Generic;
using Application.Results;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ITodoItemRepositoryAsync : IRepositoryAsync<TodoItem, int>
    {
        Task<PaginatedResult<GetAllTasksResponse>> GetReportAsync(PagedRequestParameters requestParameters, CancellationToken cancellationToken);

    }
}
