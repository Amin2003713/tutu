using Application.Sliders.CommandAndQueries;
using Application.Sliders.Responses;

namespace Application.Sliders.Interfaces;

public interface ISliderService
{
    Task<ApiResult?> CreateSlider(CreateSliderCommand command);
    Task<ApiResult?> EditSlider(EditSliderCommand command);
    Task<ApiResult?> DeleteSlider(long sliderId);

    Task<ApiResult<SliderDto>?> GetSliderById(long sliderId);
    Task<ApiResult<List<SliderDto>>?> GetSliders();
}