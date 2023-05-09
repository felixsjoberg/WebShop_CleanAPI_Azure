using Application.Products.Queries;
using FluentValidation;

namespace Application.Authentication.Queries.Login;
public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}