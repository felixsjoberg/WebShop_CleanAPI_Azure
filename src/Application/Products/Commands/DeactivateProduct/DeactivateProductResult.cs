namespace Application.Products.Commands.DeactivateProduct;

public class DeactivateProductResult
{
    public bool Success { get; }

    public DeactivateProductResult(bool success)
    {
        Success = success;
    }
}