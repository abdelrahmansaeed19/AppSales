using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Modules.Users.Commands;
using Application.Modules.Users.Queries;
using API.Responses;
using Application.Modules.Users.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetUser), new { id = userId }, new ApiResponse<long>(userId));
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
        public async Task<IActionResult> GetUser(long id)
        {
            var query = new GetUserByIdQuery(id);
            try
            {
                UserDto user = await _mediator.Send(query);

                return Ok(new ApiResponse<UserDto>(user));
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

        [HttpGet("getByTenant/{tenantId}")]
        public async Task<IActionResult> GetUsersByTenant(long tenantId)
        {
            var query = new GetUsersByTenantQuery(tenantId);
            try
            {
                List<UserDto> users = await _mediator.Send(query);

                return Ok(new ApiResponse<List<UserDto>>(users));
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

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok(new ApiResponse<string>("User updated successfully."));
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
        public async Task<IActionResult> DeactivateUser(long id)
        {
            var command = new DeactivateUserCommand(id);
            try
            {
                await _mediator.Send(command);
                return Ok(new ApiResponse<string>("User deactivated successfully."));
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var command = new DeleteUserCommand(id);
            try
            {
                await _mediator.Send(command);
                return Ok(new ApiResponse<string>("User deleted successfully."));
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