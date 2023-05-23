using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProdcutCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public UpdateProdcutCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException();
        var categories = await _categoryRepository.GetAllAsync();
        var amountOfCategories = categories.Count();
        if (request.CategoryId > amountOfCategories)
        {
            throw new OutOfCategoryRange();
        }
        var modifiedProduct = new Product(
            new ProductId(request.Id),
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.CategoryId,
            request.ImageUrl
        );
        await _productRepository.UpdateAsync(modifiedProduct);

        return Unit.Value;
    }
}