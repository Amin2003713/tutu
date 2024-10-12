using System.Globalization;
using Application.Extensions;
using BlazorHero.CleanArchitecture2.Client.Infrastructure.Managers.Preferences;
using BlazorHero.CleanArchitecture2.Client.Infrastructure.Settings;
using BlazorHero.CleanArchitecture2.Shared.Constants.Localization;
using Infra.Extensions;
using MyShop.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyShop.Client.AuthenticationProvider;
using MyShop.Client.Extensions;

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