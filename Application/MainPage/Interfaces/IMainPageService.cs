using Application.MainPage.Responses;

namespace Application.MainPage.Interfaces;

public interface IMainPageService
{
    Task<MainPageDto> GetMainPageData();
}