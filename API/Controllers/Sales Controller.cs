using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Modules.Sales.Commands;
using Application.Modules.Sales.Queries;
using Application.Modules.Sales.DTOs;
using Domain.Enums;
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
        // 1. COMMANDS
        // =================================================================

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                // Assuming Send returns an ID or object, capture it if needed
                var result = await _mediator.Send(command);

                return Ok(new ApiResponse<string>("Order Requested Successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok(new ApiResponse<string>("Order Updated Successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpPost("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequestDto reqestDto)
        {
            try
            {
                await _mediator.Send(new CancelOrderCommand(reqestDto.Id, reqestDto.Reason));

                return Ok(new ApiResponse<string>("Order Canceled Successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        // =================================================================
        // 2. QUERIES
        // =================================================================

        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            try
            {
                OrderDto order = await _mediator.Send(new GetOrderByIdQuery(id));

                return Ok(new ApiResponse<OrderDto>(order));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpGet("BranchOrders/{branchId}")]
        public async Task<IActionResult> GetOrdersByBranch(long branchId)
        {
            try
            {
                List<OrderSummaryDto> orders = await _mediator.Send(new GetOrderByBranchQuery(branchId));

                return Ok(new ApiResponse<List<OrderSummaryDto>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpGet("Branch/{branchId}/Status/{status}")]
        public async Task<IActionResult> GetOrdersByBranchAndStatus(long branchId, OrderStatus status)
        {
            try
            {
                List<OrderSummaryDto> orders = await _mediator.Send(new GetOrderByStatusQuery(branchId, status));

                return Ok(new ApiResponse<List<OrderSummaryDto>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }

        [HttpGet("Tenant/{tenantId}")]
        public async Task<IActionResult> GetOrdersByTenant(long tenantId)
        {
            try
            {
                List<OrderSummaryDto> orders = await _mediator.Send(new GetOrderByTenantQuery(tenantId));

                return Ok(new ApiResponse<List<OrderSummaryDto>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Data = ex.Message });
            }
        }
    }
}