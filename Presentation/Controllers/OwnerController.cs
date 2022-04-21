using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public OwnerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            try
            {
                var owners = await _serviceManager.OwnerService.GetAllOwnersAsync(trackChanges: false);

                return Ok(owners);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
           
        }
    }
}