// using Application.Common.Interfaces.Persistence;
// using MediatR;

// namespace Application.Products.Queries.SearchProducts;

// public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchProductsResult>
// {
//     private readonly IProductRepository _productRepository;

//     public SearchProductsQueryHandler(IProductRepository productRepository)
//     {

//         _productRepository = productRepository;
//     }

//     public async Task<SearchProductsResult> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
//     {
//         var query = _productRepository.AsQueryable();

//         if (!string.IsNullOrEmpty(searchTerm))
//         {
//             query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
//         }

//         if (!string.IsNullOrEmpty(category))
//         {
//             query = query.Where(p => p.Category == category);
//         }

//         if (minPrice != null)
//         {
//             query = query.Where(p => p.Price >= minPrice);
//         }

//         if (maxPrice != null)
//         {
//             query = query.Where(p => p.Price <= maxPrice);
//         }

//         var results = await query.ToListAsync();

//         var result = await _productRepository.
//         if (result is not TransferAggregate transferDetails)
//         {
//             throw new InternalServerError();
//         }
//         return new AddAccountCreditResult(result);
//     }
// }