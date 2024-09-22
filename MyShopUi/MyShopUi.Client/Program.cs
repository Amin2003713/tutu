using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.RegisterApplicationDependency();
builder.Services.RegisterInfraDependency();

await builder.Build().RunAsync();