using Application.Categories.Commands.AddCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Common.DTOs;
using Application.Categories.Queries.GetAllCategories;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Categories.AddCategory;

namespace Presentation.API.Controllers;
[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[Controller]")]
public class CategoryController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CategoryController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query);
        var response = _mapper.Map<CategoryDtoList>(result);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(AddCategoryRequest request)
    {
        var command = _mapper.Map<AddCategoryCommand>(request);
        var result = await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var command = new DeleteCategoryCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}