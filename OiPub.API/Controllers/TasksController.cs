using Application.DTOs.Generic;
using Application.DTOs.Request;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.Results;

namespace TaskManager.API.Controllers
{
    public class TasksController : ControllerBase
    {
        private readonly ITodoItemRepositoryAsync _papersRepository;
        private readonly IUnitOfWork<int> _unitOfWork;

        public TasksController(ITodoItemRepositoryAsync papersRepository, IUnitOfWork<int> unitOfWork)
        {
            _papersRepository = papersRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            PagedRequestParameters pagedRequest = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchString = searchString,
                OrderBy = orderBy
            };
            var result = await _papersRepository.GetReportAsync(pagedRequest, new CancellationToken());
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateTaskRequest requestData)
        {
            TodoItem data = new()
            {
                Name = requestData.Name,
                Points = requestData.Points,
                Status = requestData.Status,
                AssigenedTo = requestData.AssigenedTo,

            };
            await _unitOfWork.TodoItemRepository.AddAsync(data);
            await _unitOfWork.Commit(new CancellationToken());
            return Ok(await Result<int>.SuccessAsync(data.Id, "Task Saved"));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paperData = await _unitOfWork.TodoItemRepository.GetByIdAsync(id, new CancellationToken());
            return Ok(await Result<TodoItem>.SuccessAsync(paperData));
        }



    }
}
