using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Owners.Commands;
using Application.Features.Owners.Queries;
using LoggerService.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
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

        [HttpGet("{id:guid}", Name = "GetOwnerByIdAsync")]
        public async Task<IActionResult> GetOwnerByIdAsync(Guid id)
        {
            var owner = await _mediator.Send(new GetOwnerByIdQuery(id, trackChanges: false));

            return Ok(owner);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerCreationDto ownerCreationDto)
        {
            var owner = await _mediator.Send(new CreateOwnerCommand(ownerCreationDto));

            return CreatedAtRoute("GetOwnerById", new {id = owner.Id}, owner);
        }

        [HttpGet("collection/({ownerIds})", Name = "GetOwnerCollection")]
        public IActionResult GetOwnerCollectionAsync(IEnumerable<Guid> ownerIds)
        {
            var owners = _mediator.Send(new GetAllOwnersByIdsQuery(ownerIds, trackChanges: false));

            return Ok(ownerIds);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateOwnerCollection([FromBody] IEnumerable<OwnerCreationDto> ownerCollection)
        {
            _loggerManager.LogWarn("invoked");
            var response = await _mediator.Send(new CreateOwnerCollectionCommand(ownerCollection));

            return CreatedAtRoute("GetOwnerCollection", new {response.ownerIds}, response.owners);
        }
    }
}