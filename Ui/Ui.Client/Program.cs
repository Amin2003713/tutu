using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();

builder.Services.RegisterApplicationDependency();
builder.Services.RegisterInfraDependency();


await builder.Build().RunAsync();