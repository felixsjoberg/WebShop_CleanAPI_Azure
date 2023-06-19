using Application.Categories.Commands.AddCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Common.DTOs;
using Application.Categories.Queries.GetAllCategories;
using Application.Customers.Queries.GetAllCustomers;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Categories.AddCategory;
using Presentation.Contracts.Customers.GetAllCustomers;

namespace Presentation.API.Controllers;
[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[Controller]")]
public class CustomerController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CustomerController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllCustomersQuery();
        var result = await _mediator.Send(query);
        var response = _mapper.Map<GetAllCustomersResponse>(result);
        return Ok(response);
    }
}