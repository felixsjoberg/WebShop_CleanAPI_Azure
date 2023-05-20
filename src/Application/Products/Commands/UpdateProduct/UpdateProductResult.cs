namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductResult
{
    public bool Success { get; }

    public UpdateProductResult(bool success)
    {
        Success = success;
    }
}