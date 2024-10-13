using Blazored.LocalStorage;
using Domain.Common.Api;
using Infra.Extensions.Settings;
using Infra.Utils.Constants.Storage;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Infra.Extensions.Preferences;

public class ClientPreferenceManager : IClientPreferenceManager
{
    private readonly IStringLocalizer<ClientPreferenceManager> _localizer;
    private readonly ILocalStorageService _localStorageService;

    public ClientPreferenceManager(
        ILocalStorageService localStorageService,
        IStringLocalizer<ClientPreferenceManager> localizer)
    {
        _localStorageService = localStorageService;
        _localizer = localizer;
    }

    public async Task<bool> ToggleDarkModeAsync()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            preference.IsDarkMode = !preference.IsDarkMode;
            await SetPreference(preference);
            return !preference.IsDarkMode;
        }

        return false;
    }

    public async Task<ApiResult> ChangeLanguageAsync(string languageCode)
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            preference.LanguageCode = languageCode;
            await SetPreference(preference);
            return new ApiResult
            {
                IsSuccess = true
            };
        }

        return new ApiResult
        {
            IsSuccess = false
        };
    }

    public async Task<MudTheme> GetCurrentThemeAsync()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
            if (preference.IsDarkMode)
                return BlazorHeroTheme.DarkTheme;

        return BlazorHeroTheme.DefaultTheme;
    }

    public async Task<IPreference> GetPreference()
    {
        return await _localStorageService.GetItemAsync<ClientPreference>(StorageConstants.Local.Preference) ??
               new ClientPreference();
    }

    public async Task SetPreference(IPreference preference)
    {
        await _localStorageService.SetItemAsync(StorageConstants.Local.Preference, preference as ClientPreference);
    }

    public async Task<bool> ToggleLayoutDirection()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            preference.IsRTL = !preference.IsRTL;
            await SetPreference(preference);
            return preference.IsRTL;
        }

        return false;
    }

    public async Task<bool> IsRTL()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
            if (preference.IsDarkMode)
                return false;

        return preference.IsRTL;
    }
}