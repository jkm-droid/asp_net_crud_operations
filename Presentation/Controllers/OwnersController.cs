using System;
using System.Threading.Tasks;
using Application.Features.Owners.Commands;
using Application.Features.Owners.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
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

            return CreatedAtAction("GetOwnerById", new {id = owner.Id}, owner);
        }
    }
}