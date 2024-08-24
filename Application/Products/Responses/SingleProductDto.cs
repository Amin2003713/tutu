namespace Domain.Products;

public class SingleProductDto
{
    public ProductDto ProductDto { get; set; }
    public List<InventoryDto> Inventories { get; set; }
}