﻿using Application.DTOs.Generic;
using Application.DTOs.Response;
using Application.Extensions;
using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories;
using Application.Results;
using Domain.Entities;
using Infrastructure.Repositories.Generic;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TodoItemRepository : RepositoryAsync<TodoItem, int>, ITodoItemRepositoryAsync
    {
        public TodoItemRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResult<GetAllTasksResponse>> GetReportAsync(PagedRequestParameters requestParameters, CancellationToken cancellationToken)
        {
            Expression<Func<TodoItem, GetAllTasksResponse>> expression = e => new GetAllTasksResponse
            {
                Id = e.Id,
                Authors = e.Name,
                DatePublished = e.Points,
                NumberofCitations = e.NumberofCitations,
                ReferenceCount = e.ReferenceCount,
                Title = e.Title,
                NumberOfRead = e.NumberOfRead
            };
            var data = Entities
                       .Select(expression);
            if (!string.IsNullOrWhiteSpace(requestParameters.SearchString))
            {
                data = data.Where(x => x.Authors.ToLower().Contains(requestParameters.SearchString.ToLower()) || x.Title.ToLower().Contains(requestParameters.SearchString.ToLower()));
            }


            if (requestParameters.OrderBy != null && requestParameters.OrderBy.Any())
            {
                data = data.OrderBy(requestParameters.OrderBy);

            }
            var paginatedList = await data.ToPaginatedListAsync(requestParameters.PageNumber, requestParameters.PageSize, cancellationToken);
            return paginatedList;
        }





    }
}
