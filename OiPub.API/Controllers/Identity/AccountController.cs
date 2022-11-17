using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Controllers.Base;

namespace TaskManager.API.Controllers.Identity
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController<AccountController>
    {
        #region >>> Properties <<<
        private readonly IAccountService _accountService;
        #endregion


        #region >>> Constructor <<<
        /// <summary>
        /// Inject identity service to identity controller 
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion


        #region >>> Action Method <<<
        /// <summary>
        /// Get All Accounts
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Accounts.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, CancellationToken cancellationToken, string orderBy = null)
        {
            var requestParameters = new PagedRequestParameters()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchString = searchString,
                OrderBy = orderBy
            };
            var result = await _accountService.GetAllAsync(requestParameters, cancellationToken);
            return Ok(result);
        }



        /// <summary>
        /// Create a Account
        /// </summary>
        /// <param name="command"></param>
        /// <param name="origin"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Accounts.Create)]
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateUserRequest command, string origin)
        {
            return Ok(await _accountService.CreateAsync(command, origin));
        }


        /// <summary>
        /// Update a User
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Accounts.Create)]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateUserRequest command)
        {
            var result = await _accountService.UpdateAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Accounts.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _accountService.DeleteAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// Get a User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Account.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _accountService.GetById(id);
            return Ok(result);
        }



        /// <summary>
        /// Get All Consumer Async
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Accounts.View)]
        [HttpGet("GetAllConsumerAsync")]
        public async Task<IActionResult> GetAllConsumerAsync(int pageNumber, int pageSize, string searchString, CancellationToken cancellationToken, string orderBy = null)
        {
            var requestParameters = new PagedRequestParameters()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchString = searchString,
                OrderBy = orderBy
            };
            var result = await _accountService.GetAllConsumerAsync(requestParameters, cancellationToken);
            return Ok(result);
        }

        #endregion

    }
}
