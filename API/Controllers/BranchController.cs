using Application.Modules.Branches.Commands;
using Application.Modules.Branches.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Responses;
using Application.Modules.Branches.DTOs;

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
                var branchId = await _mediator.Send(command);

                return Ok(new ApiResponse<long>(branchId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpGet("BranchesByTenant/{id}")]
        public async Task<IActionResult> GetBranchByTenantId(int id)
        {
            var query = new GetBranchesByTenantQuery(id);

            try
            {
                var result = await _mediator.Send(query);

                return Ok(new ApiResponse<List<BranchDto>>(result)); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok(new ApiResponse<string>("Branch updated successfully."));
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
        public async Task<IActionResult> DeactivateBranch(int id)
        {
            var command = new DeactivateBranchCommand(id);
            try
            {
                await _mediator.Send(command);
                return Ok(new ApiResponse<string>("Branch deactivated successfully."));
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
    }
}