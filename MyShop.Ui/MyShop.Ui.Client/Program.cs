using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyShop.Ui.Client.AuthenticationProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthStateProvider>();



await builder.Build().RunAsync();