using API.Responses;
using Application.Modules.Customers.Commands;
using Application.Modules.Customers.DTO;
using Application.Modules.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  
        [ApiController]
        [Route("customers")]
        public class CustomersController : ControllerBase
        {
            private readonly IMediator _mediator;

            public CustomersController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
                return Ok(customer);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
            {
                command.Id = id;
                var result = await _mediator.Send(command);
                return Ok(result);
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand(id));
            return Ok(new ApiResponse<string> { Success = true, Data = "Customer deleted successfully" });
        }

        [HttpGet("{id}/statement")]
        public async Task<IActionResult> GetStatement(int id)
        {
            var statement = await _mediator.Send(new GetCustomerStatementQuery(id));

            if (statement == null)
                return NotFound(new ApiResponse<string> { Success = false, Data = "Customer not found" });

            return Ok(new ApiResponse<CustomerStatementResponse>(statement));
        }
    }

    }
