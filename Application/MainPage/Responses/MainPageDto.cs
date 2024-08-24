using Application.Products.Responses;
using Application.Sliders.Responses;

namespace Application.MainPage.Responses;

public class MainPageDto
{
    public List<SliderDto> Sliders { get; set; }
    public List<BannerResponses> Banners { get; set; }
    public List<ProductShopDto> SpectialProducts { get; set; }
    public List<ProductShopDto> LatestProducts { get; set; }
    public List<ProductShopDto> TopVisitProducts { get; set; }
}