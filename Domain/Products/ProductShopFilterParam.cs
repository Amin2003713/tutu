using Domain.Common.Filter;

namespace Domain.Products;

public class ProductShopFilterParam : BaseFilterParam
{
    public string? CategorySlug { get; set; } = "";
    public string? Search { get; set; } = "";
    public bool OnlyAvailableProducts { get; set; } = false;
    public bool? JustHasDiscount { get; set; } = false;
    public ProductSearchOrderBy SearchOrderBy { get; set; } = ProductSearchOrderBy.Cheapest;
}