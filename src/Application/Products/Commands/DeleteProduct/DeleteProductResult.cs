namespace Application.Products.Commands.DeleteProduct;

public class DeleteProductResult
{
    public bool Success { get; }

    public DeleteProductResult(bool success)
    {
        Success = success;
    }
}