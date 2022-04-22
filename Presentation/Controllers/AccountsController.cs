using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.Accounts.Queries;
using Application.Features.Owners.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/owners/{ownerId}/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController( IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var accounts = await _mediator.Send(new GetAllAccountsQuery());
            
            return Ok(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnerAccountsAsync(Guid ownerId)
        {
            var ownerAccounts = await _mediator.Send(new GetOwnerAccountsQuery(ownerId, trackChanges: false));

            return Ok(ownerAccounts);
        }
    }
}