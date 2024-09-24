using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddAuthorizationCore();



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
await builder.Build().RunAsync();