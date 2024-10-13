using MudBlazor;

namespace Infra.Extensions.Preferences;

public interface IClientPreferenceManager : IPreferenceManager
{
    Task<MudTheme> GetCurrentThemeAsync();

    Task<bool> ToggleDarkModeAsync();
}