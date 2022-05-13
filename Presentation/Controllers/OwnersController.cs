using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Owners.Commands;
using Application.Features.Owners.Queries;
using Domain.Exceptions;
using LoggerService.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
using Presentation.ActionFilters;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerManager _loggerManager;
        public OwnersController(IMediator mediator, ILoggerManager loggerManager)
        {
            _mediator = mediator;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnersAsync()
        {
            var owners = await _mediator.Send(new GetAllOwnersQuery(trackChanges: false));

            return Ok(owners);
        }

        [HttpGet("{id:guid}", Name = "GetOwnerById")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var owner = await _mediator.Send(new GetOwnerByIdQuery(id, trackChanges: false));

            return Ok(owner);
        }
        
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerCreationDto ownerCreationDto)
        {
            var owner = await _mediator.Send(new CreateOwnerCommand(ownerCreationDto));

            return CreatedAtRoute("GetOwnerById", new {id = owner.Id}, owner);
        }

        [HttpGet("collection/({ownerIds})", Name = "GetOwnerCollection")]
        public IActionResult GetOwnerCollectionAsync(IEnumerable<Guid> ownerIds)
        {
            var owners = _mediator.Send(new GetAllOwnersByIdsQuery(ownerIds, trackChanges: false));

            return Ok(owners);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOwnerCollection([FromBody] IEnumerable<OwnerCreationDto> ownerCollection)
        {
            var response = await _mediator.Send(new CreateOwnerCollectionCommand(ownerCollection));

            return CreatedAtRoute("GetOwnerCollection", new {response.ownerIds}, response.owners);
        }

        [HttpPut("{ownerId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOwner([FromBody] OwnerUpdateDto ownerUpdateDto, Guid ownerId)
        {
            await _mediator.Send(new UpdateOwnerCommand(ownerId, ownerUpdateDto, trackChanges: true));

            return NoContent();
        }

        [HttpDelete("{ownerId:guid}")]
        public async Task<IActionResult> DeleteOwner(Guid ownerId)
        {
            await _mediator.Send(new DeleteOwnerCommand(ownerId, trackChanges: false));

            return NoContent();
        }
    }
}