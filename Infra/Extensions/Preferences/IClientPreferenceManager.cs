using MudBlazor;
using System.Threading.Tasks;
using Domain.Common.Api;

namespace BlazorHero.CleanArchitecture2.Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}

public interface IPreference
{
    public string LanguageCode { get; set; }
}

public interface IPreferenceManager
{
    Task SetPreference(IPreference preference);

    Task<IPreference> GetPreference();

    Task<ApiResult> ChangeLanguageAsync(string languageCode);
}