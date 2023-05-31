using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.AddCategory;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name);
        await _categoryRepository.AddAsync(category);

        return Unit.Value;
    }
}