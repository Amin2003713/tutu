using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Extensions;
using Blazored.LocalStorage;
using BlazorProjetc.UI.Client.Common.Auth;
using BlazorProjetc.UI.Client.Common.Services;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});

builder.Services.AddScoped<WebAssemblyLocalStorage>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();