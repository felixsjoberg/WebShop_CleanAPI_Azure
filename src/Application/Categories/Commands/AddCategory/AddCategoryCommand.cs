using MediatR;
namespace Application.Categories.Commands.AddCategory;
public record AddCategoryCommand(string Name): IRequest<Unit>;