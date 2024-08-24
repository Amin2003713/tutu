using Domain.Common.@base;

namespace Domain.Products;

public class ProductFilterData : BaseDto
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Slug { get; set; }
}