using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;


        public ProductController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
        // [HttpGet]
        // [Route("api/product/search")]
        // public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string searchTerm, string category, decimal? minPrice, decimal? maxPrice)
        // {
        //     // 1. Map request to command
        //     var command = new SearchProductsCommand
        //     {
        //         SearchTerm = searchTerm,
        //         Category = category,
        //         MinPrice = minPrice,
        //         MaxPrice = maxPrice
        //     };
        //     // 2. Send command to mediator
        //     var authResult = await _mediator.Send(command);
        //     // 3. Map result to response
        //     var results = _mapper.Map<IEnumerable<Product>>(authResult);
        //     // move logic to service layer.
        //     return Ok(results);
        // }

    }
}