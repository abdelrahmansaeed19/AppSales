using Application.Modules.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ---------------- Register -------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            try
            {
                // Sends verification code only; user not yet created
                await _mediator.Send(command);
                return Ok(new { message = "Verification email sent. Please check your inbox to complete registration." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }

        // ---------------- Verify Email ----------------
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
        {
            try
            {
                // Creates user after successful verification
                await _mediator.Send(command);
                return Ok(new { message = "Email verified. Your account is pending admin approval" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("approve-user/{id}")]
        public async Task<IActionResult> ApproveUser(long id)
        {
            await _mediator.Send(new ApproveUserCommand(id));
            return Ok(new { message = "User approved successfully." });
        }

        // ---------------- Resend Verification ----------------
        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Verification email resent. Please check your inbox." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }

        // ---------------- Login ----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }

        // ---------------- Refresh Token ----------------
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }



        // ---------------- Forgot Password ----------------
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Reset code sent to your email." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }

        // ---------------- Reset Password ----------------
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Password reset successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong", detail = ex.Message });
            }
        }

    }
}
