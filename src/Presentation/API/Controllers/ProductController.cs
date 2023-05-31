using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeactivateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Common.DTOs;
using Application.Products.Queries.SearchProducts;
using Application.Products.Response.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Products.CreateProduct;
using Presentation.Contracts.Products.SearchProduct;
using Presentation.Contracts.Products.UpdateProduct;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            var response = _mapper.Map<ProductDtoList>(result);
            return Ok(response);
        }
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductsRequest request)
        {
            var query = _mapper.Map<SearchProductsQuery>(request);

            var result = await _mediator.Send(query);

            var response = _mapper.Map<ProductDtoList>(result);

            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var command = _mapper.Map<CreateProductCommand>(request);

            var result = await _mediator.Send(command);

            var response = _mapper.Map<ProductDto>(result);
            return Ok(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);

            var result = await _mediator.Send(command);

            return Ok();
        }
        [HttpPatch("{id}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateProduct(int id)
        {
            var command = new DeactivateProductCommand(id);

            await _mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}