using Domain.Entities;

namespace Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesResult(IEnumerable<Category> Categories);