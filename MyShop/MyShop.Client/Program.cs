using Application.Extensions;
using Infra.Extensions;
using MyShop.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyShop.Client.AuthenticationProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();


builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthStateProvider>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMudServices();
builder.Services.AddMudBlazorDialog();

await builder.Build().RunAsync();