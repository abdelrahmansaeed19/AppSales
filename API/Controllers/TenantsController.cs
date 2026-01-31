using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Modules.Tenants.Commands;
using Application.Modules.Tenant.Queries;
using API.Responses;
using Application.Modules.Tenant.DTOs;

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
                // Return Created (201) but with the standard ApiResponse structure
                return CreatedAtAction(nameof(GetTenant), new { id = tenantId }, new ApiResponse<long>(tenantId));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTenant(long id)
        {
            try
            {
                var query = new GetTenantByIdQuery(id);

                TenantDto tenant = await _mediator.Send(query);

                if (tenant == null)
                {
                    return NotFound(new ApiResponse<string> { Success = false, Data = "Tenant not found." });
                }
                return Ok(new ApiResponse<TenantDto>(tenant));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTenant([FromBody] UpdateTenantCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok(new ApiResponse<string>("Tenant updated successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateTenant(long id)
        {
            var command = new DeactivateTenantCommand(id);
            try
            {
                await _mediator.Send(command);
                return Ok(new ApiResponse<string>("Tenant deactivated successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            try
            {
                var query = new GetAllTenantsQuery();
                var tenants = await _mediator.Send(query);
                return Ok(new ApiResponse<IEnumerable<TenantDto>>(tenants));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

    }
}