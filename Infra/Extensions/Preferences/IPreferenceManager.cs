using Domain.Common.Api;

namespace Infra.Extensions.Preferences;

public interface IPreferenceManager
{
    Task SetPreference(IPreference preference);

    Task<IPreference> GetPreference();

    Task<ApiResult> ChangeLanguageAsync(string languageCode);
}