using Application.Common;
using Application.Sliders.CommandAndQueries;
using Application.Sliders.Interfaces;
using Application.Sliders.Responses;
using Domain.Common.Api;
using Infra.Utils;

namespace Infra.Sliders.Implantations;

public class SliderService(IBaseHttpClient client) : ISliderService
{
    public async Task<ApiResult?> CreateSlider(CreateSliderCommand command)
    {
        return await client.PostMultipartAsync<CreateSliderCommand, ApiResult>(
            SliderRoute.CreateSlider, command);
    }

    public async Task<ApiResult?> EditSlider(EditSliderCommand command)
    {
        return await client.PutMultipartAsync<EditSliderCommand, ApiResult>(
            SliderRoute.CreateSlider, command);
    }

    public async Task<ApiResult?> DeleteSlider(long sliderId)
    {
        return await client.DeleteAsync<ApiResult>(
            SliderRoute.DeleteSliderById.BuildRequestUrl([sliderId])!);
    }

    public async Task<ApiResult<SliderDto>?> GetSliderById(long sliderId)
    {
        return await client.GetAsync<ApiResult<SliderDto>>(
            SliderRoute.GetSliderById.BuildRequestUrl([sliderId])!);
    }

    public async Task<ApiResult<List<SliderDto>>?> GetSliders()
    {
        return await client.GetAsync<ApiResult<List<SliderDto>>>(
            SliderRoute.GetAllSliders);
    }
}