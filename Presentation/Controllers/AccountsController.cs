using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var accounts = await _mediator.Send(new GetAllAccountsQuery());
            
            return Ok(accounts);
        }
    }
}