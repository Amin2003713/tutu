using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyShop.Ui.Client.AuthenticationProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthStateProvider>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMudBlazorDialog();
    
await builder.Build().RunAsync();