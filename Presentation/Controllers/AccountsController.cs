using System;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Features.Accounts.Commands;
using Application.Features.Accounts.Queries;
using LoggerService.Abstractions;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [Route("api/owners/{ownerId}/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerManager _loggerManager;

        public AccountsController( IMediator mediator, ILoggerManager loggerManager)
        {
            _mediator = mediator;
            _loggerManager = loggerManager;
        }

        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var accounts = await _mediator.Send(new GetAllAccountsQuery());
            
            return Ok(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnerAccountsAsync(Guid ownerId, [FromQuery] AccountParameters accountParameters)
        {
            var pagedResult = await _mediator.Send(new GetOwnerAccountsQuery(ownerId, accountParameters, trackChanges: false));
            
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.pagingMetaData));
            
            return Ok(pagedResult.accounts);
        }

        [HttpGet("{accountId:guid}", Name = "GetOwnerAccount")]
        public async Task<IActionResult> GetOwnerAccountAsync(Guid ownerId, Guid accountId)
        {
            var account = await _mediator.Send(new GetOwnerAccountByIdQuery(ownerId, accountId, trackChanges: false));

            return Ok(account);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAccountAsync(Guid ownerId, [FromBody] AccountCreationDto accountCreationDto)
        {
            var account = await _mediator.Send(new CreateAccountCommand(ownerId, accountCreationDto));

            return CreatedAtAction("GetOwnerAccount", new {ownerId, accountId = account.Id}, account);
        }
        
        [HttpDelete("{accountId:guid}")]
        public async Task<IActionResult> DeleteAccountForOwner(Guid ownerId, Guid accountId)
        {
            await _mediator.Send(new DeleteAccountCommand(ownerId, accountId, trackChanges: false));

            return NoContent();
        }

        [HttpPut("{accountId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountUpdateDto account, Guid ownerId, Guid accountId)
        {
            await _mediator.Send(new UpdateAccountCommand(account, ownerId, accountId, ownerTrackChanges: false,
                accountTrackChanges: true));

            return NoContent();
        }

        [HttpPatch("{accountId:guid}")]
        public async Task<IActionResult> PartiallyUpdateAccount(Guid ownerId, Guid accountId,
            [FromBody] JsonPatchDocument<AccountUpdateDto> patchDocument)
        {
            if (patchDocument is null)
                return BadRequest("Account patch object is null");
            
            var response = await _mediator.Send(new GetAccountForPatchQuery(ownerId, accountId, ownerTrackChanges: false,
                accountTrackChanges: true));
            
            patchDocument.ApplyTo(response.accountPatch, ModelState);

            TryValidateModel(response.accountPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _mediator.Send(new PartialUpdateAccountCommand(response.accountPatch, response.account));

            return NoContent();
        }
    }
}