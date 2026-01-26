using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Modules.Users.Commands;
using Application.Modules.Users.Queries;


namespace API.Controllers
{
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

                return CreatedAtAction(nameof(CreateUser), new { UserId = userId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var query = new GetUserByIdQuery(id);
            try
            {
                var user = await _mediator.Send(query);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        [HttpGet("getByTenant/{tenantId}")]
        public async Task<IActionResult> GetUsersByTenant(long tenantId)
        {
            var query = new GetUsersByTenantQuery(tenantId);
            try
            {
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.UserId)
            {
                return BadRequest(new { Error = "User ID mismatch." });
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
        public async Task<IActionResult> DeactivateUser(long id)
        {
            var command = new DeactivateUserCommand(id);
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
