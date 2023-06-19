using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =  await _categoryRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException();
        await _categoryRepository.DeleteAsync(category);
        return Unit.Value;
    }
}