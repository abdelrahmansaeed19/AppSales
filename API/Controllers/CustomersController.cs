using API.Responses;
using Application.Exceptions;
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
            try
            {
                var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
                return Ok(customer);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
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
            try
            {
                command.Id = id;
                var result = await _mediator.Send(command);
                return Ok(result); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteCustomerCommand(id));
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
        }

        [HttpGet("{id}/statement")]
        public async Task<IActionResult> GetStatement(int id)
        {
            try
            {
                var statement = await _mediator.Send(new GetCustomerStatementQuery(id));
                return Ok(statement); // return the object directly
            }
            catch (Application.Exceptions.NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // simple JSON message
            }
        }

    }

}
