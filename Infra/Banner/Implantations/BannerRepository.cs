using Application.Banner.CommandAndQueries;
using Application.Banner.Interfaces;
using Application.Banner.Responses;
using Application.Common;
using Domain.Common.Api;
using Infra.Utils;

namespace Infra.Banner.Implantations;

public class BannerRepository(IBaseHttpClient client) : IBannerRepository
{
    public async Task<ApiResult?> CreateBanner(CreateBannerCommand command)
    {
        return await client.PostAsync<CreateBannerCommand, ApiResult>(BannerRouts.PostBanner, command);
    }

    public async Task<ApiResult?> EditBanner(EditBannerCommand command)
    {
        return await client.PutAsync<EditBannerCommand, ApiResult>(BannerRouts.PutBanner, command);
    }

    public async Task<ApiResult> DeleteBanner(BannerIdCommand command)
    {
        return (await client.DeleteAsync<ApiResult>(BannerRouts.DeleteBannerById.BuildRequestUrl([command.Id])!))!;
    }

    public async Task<ApiResult<BannerResponses?>> GetBannerById(BannerIdCommand command)
    {
        return (await client.GetAsync<ApiResult<BannerResponses>>(
            BannerRouts.GetBannerById.BuildRequestUrl([command.Id])!))!;
    }

    public async Task<ApiResult<List<BannerResponses>>> GetBanners()
    {
        return (await client.GetAsync<ApiResult<List<BannerResponses>>>(BannerRouts.GetBanner))!;
    }
}