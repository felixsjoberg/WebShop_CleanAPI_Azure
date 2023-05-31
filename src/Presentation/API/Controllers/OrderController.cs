using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.UpdateOrder;
using Application.Orders.Common.DTOs;
using Application.Orders.Queries.GetAllOrders;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Service;
using Presentation.Contracts;
using Presentation.Contracts.Orders.CreateOrder;
using Presentation.Contracts.Orders.UpdateOrder;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public OrderController(ISender mediator, IMapper mapper, JwtService jwtService)
        {
            _jwtService = jwtService;
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        //Change to Admin
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllOrdersQuery();
            var result = await _mediator.Send(query);
            var response = _mapper.Map<OrderDtoList>(result);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            Guid userId = _jwtService.ExtractJwt();
            var requestData = _mapper.Map<CreateOrderData>(request);
            requestData = requestData with { UserId = userId };

            var command = _mapper.Map<CreateOrderCommand>(requestData);

            var result = await _mediator.Send(command);

            var response = _mapper.Map<OrderDto>(result);

            return Ok(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(UpdateOrderRequest request)
        {
            var command = _mapper.Map<UpdateOrderCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteOrderCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}