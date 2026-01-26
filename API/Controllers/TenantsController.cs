using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Modules.Tenants.Commands;
using Application.Modules.Tenant.Queries;

namespace API.Modules.Tenant
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantCommand command)
        {
            try
            {
                var tenantId = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreateTenant), new { TenantId = tenantId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTenant(long id)
        {
            var query = new GetTenantByIdQuery(id);

            var tenant = await _mediator.Send(query);
            if (tenant == null)
            {
                return NotFound(new { Error = "Tenant not found." });
            }
            return Ok(tenant);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTenant(long id, [FromBody] UpdateTenantCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Error = "Tenant ID mismatch." });
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
        public async Task<IActionResult> DeactivateTenant(long id)
        {
            var command = new DeactivateTenantCommand(id);
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
