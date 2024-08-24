using Domain.Sellers;

namespace Application.Products.Responses;

public class SingleProductDto
{
    public ProductDto ProductDto { get; set; }
    public List<InventoryDto> Inventories { get; set; }
}