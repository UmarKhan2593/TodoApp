using Application.DTOs.Generic;
using Application.DTOs.Request;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Results;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace OiPub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITodoItemRepositoryAsync _papersRepository;
        private readonly IUnitOfWork<int> _unitOfWork;

        public TasksController(ITodoItemRepositoryAsync papersRepository, IUnitOfWork<int> unitOfWork)
        {
            _papersRepository = papersRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all paper data
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
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


        ///// <summary>
        ///// Create a paper
        ///// </summary>
        ///// <param requestData="Data to create paper"></param>
        ///// <returns>Status 200 OK</returns>
        //[HttpPost("Create")]
        //public async Task<IActionResult> Create(CreatePaperRequest requestData)
        //{
        //    Papers data = new()
        //    {
        //        Title = requestData.Title,
        //        Authors = requestData.Authors,
        //        DatePublished = requestData.DatePublished,
        //        NumberofCitations = requestData.NumberofCitations,
        //        ReferenceCount = requestData.ReferenceCount,
        //        NumberOfRead = requestData.NumberOfRead
        //    };
        //    await _unitOfWork.papersRepository.AddAsync(data);
        //    await _unitOfWork.Commit(new CancellationToken());
        //    return Ok(await Result<int>.SuccessAsync(data.Id, "Paper Saved"));

        //}


        ///// <summary>
        ///// Get a paper by Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>Status 200 OK</returns>
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{

        //    var paperData = await _unitOfWork.papersRepository.GetByIdAsync(id, new CancellationToken());

        //    paperData.NumberOfRead = (++paperData.NumberOfRead) ?? 1;

        //    await _unitOfWork.papersRepository.UpdateAsync(paperData);


        //    await _unitOfWork.Commit(new CancellationToken());


        //    return Ok(await Result<Papers>.SuccessAsync(paperData));
        //}



    }
}
