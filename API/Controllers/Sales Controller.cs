using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Modules.Sales.Commands;
using Application.Modules.Sales.Queries;
using Application.Modules.Sales.DTOs;
using Domain.Enums;
using API.Modules.Sales;
using API.Responses;

namespace API.Modules.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sales_Controller : Controller
    {
        private readonly IMediator _mediator;

        public Sales_Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // =================================================================
        // 1. COMMANDS (الكتابة والتعديل)
        // =================================================================

        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(new ApiResponse<string> { Success = true, Data = "Order Requested Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }

        [HttpPut("UpdateStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrder(long orderId, [FromBody] UpdateOrderCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok(new ApiResponse<string> { Success = true, Data = "Order Status Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(long OrderId, [FromBody] CancelOrderRequestDto reqestDto)
        {
            try
            {
                await _mediator.Send(new CancelOrderCommand(OrderId, reqestDto.Reason));

                return Ok(new ApiResponse<string> { Success = true, Data = "Order Canceled Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }

        // =================================================================
        // 2. QUERIES (القراءة والاستعلام)
        // =================================================================

        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery(id));
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }

        [HttpGet("BranchOrders/{branchId}")]
        public async Task<IActionResult> GetOrdersByBranch(long BranchId)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrderByBranchQuery(BranchId));
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }

        [HttpGet("Branch/{branchId}/Status/{status}")]
        public async Task<IActionResult> GetOrdersByBranchAndStatus(long BranchId, OrderStatus Status)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrderByStatusQuery(BranchId, Status));


                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }

        [HttpGet("Tenant/{tenantId}")]
        public async Task<IActionResult> GetOrdersByTenant(long tenantId)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrderByTenantQuery(tenantId));

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail().Data = ex.Message);
            }
        }
    }
}
