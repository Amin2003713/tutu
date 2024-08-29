using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.RegisterApplicationDependency();
builder.Services.RegisterInfraDependency();

builder.Services.AddMudServices();

await builder.Build().RunAsync();