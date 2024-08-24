namespace Application.Products.CommandAndQueries;

public class DeleteProductImageCommand
{
    public long ImageId { get; set; }
    public long ProductId { get; set; }
}