using MediatR;

namespace Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id): IRequest<Unit>;