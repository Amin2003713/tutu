using Blazored.LocalStorage;
using Infra.Extensions.Preferences;
using Infra.User.Auth;
using Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace MyShop.Client.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        return builder;
    }

    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        builder
            .Services
            .AddLocalization(options => { options.ResourcesPath = "Resources"; })
            .AddAuthorizationCore(options => { RegisterPermissionClaims(options); })
            .AddBlazoredLocalStorage()
            .AddMudServices(configuration =>
            {
                configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                configuration.SnackbarConfiguration.ShowCloseIcon = false;
            })
            .AddScoped<ClientPreferenceManager>()
            .AddScoped<ClientStateProvider>()
            .AddScoped<AuthenticationStateProvider, ClientStateProvider>()
            .AddScoped<ILocalStorage, LocalStorage>()
            // .AddTransient<AuthenticationHeaderHandler>()
            .AddHttpContextAccessor();
        return builder;
    }


    private static void RegisterPermissionClaims(AuthorizationOptions options)
    {
        // foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c =>
        //              c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        // {
        //     var propertyValue = prop.GetValue(null);
        //     if (propertyValue is not null)
        //     {
        //         options.AddPolicy(propertyValue.ToString(),
        //             policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
        //     }
        // }
    }
}