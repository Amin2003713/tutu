using System.Globalization;
using Application.Extensions;
using Infra.Extensions;
using Infra.Extensions.Preferences;
using Infra.Extensions.Settings;
using Infra.Utils.Constants.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyShop.Ui.Extensions;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args)
    .AddRootComponents()
    .AddClientServices();
var host = builder.Build();
var storageService = host.Services.GetRequiredService<ClientPreferenceManager>();
if (storageService != null)
{
    CultureInfo culture;
    var preference = await storageService.GetPreference() as ClientPreference;
    if (preference != null)
        culture = new CultureInfo(preference.LanguageCode);
    else
        culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}


builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();


await builder.Build().RunAsync();