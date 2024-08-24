using Application.Category.Responses;
using Domain.Common.Filter;

namespace Domain.Products;

public class ProductShopResult : BaseFilter<ProductShopDto, ProductShopFilterParam>
{
    public CategoryDto? CategoryDto { get; set; }
}