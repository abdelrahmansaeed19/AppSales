using Application.Modules.Branches.Commands;
using Application.Modules.Branches.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchCommand command)
        {
            try
            {
                var tenantId = await _mediator.Send(command);
                return Ok(tenantId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("BranchesByTenant/{id}")]
        public async Task<IActionResult> GetBranchByTenantId(int id)
        {
            var query = new GetBranchesByTenantQuery(id);
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] UpdateBranchCommand command)
        {
            if (id != command.BranchId)
            {
                return BadRequest(new { Error = "Branch ID mismatch." });
            }

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateBranch(int id)
        {
            var command = new DeactivateBranchCommand(id);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}
