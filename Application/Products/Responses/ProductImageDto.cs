namespace Application.Products.Responses;

public class ProductImageDto : BaseDto
{
    public long ProductId { get; set; }
    public string ImageName { get; set; }
    public int Sequence { get; set; }
}