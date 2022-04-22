using System.Threading.Tasks;
using Application.Features.Owners.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var owners = await _mediator.Send(new GetAllOwnersQuery());

            return Ok(owners);
        }
    }
}