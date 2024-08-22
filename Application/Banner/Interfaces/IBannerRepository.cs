using Application.Banner.CommandAndQueries;
using Application.Banner.Responses;
using Domain.Common;

namespace Application.Banner.Interfaces;

public interface IBannerRepository
{
    Task<ApiResult?> CreateBanner(CreateBannerCommand command);
    Task<ApiResult?> EditBanner(EditBannerCommand command);
    Task<ApiResult> DeleteBanner(BannerIdCommand bannerId);
    Task<ApiResult<BannerResponses?>> GetBannerById(BannerIdCommand id);
    Task<ApiResult<List<BannerResponses>>> GetBanners();
}