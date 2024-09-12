using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Extensions;
using Blazored.SessionStorage;
using BlazorProjetc.UI.Services.Auth;
using Infra.Extensions;
using Infra.Utils;
using Shop.UI.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Shop.UI.Client.Common.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.RegisterApplicationDependency();
builder.Services.RegisterInfraDependency();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopLeft;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000; // 2 seconds
    config.SnackbarConfiguration.HideTransitionDuration = 500; // 0.5 seconds
    config.SnackbarConfiguration.ShowTransitionDuration = 500; // 0.5 seconds
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    config.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
    config.SnackbarConfiguration.ClearAfterNavigation = true;
    config.SnackbarConfiguration.BackgroundBlurred = true;
});

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<ILocalStorage, LocalStorage>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();