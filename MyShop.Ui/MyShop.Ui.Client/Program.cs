using Application.Extensions;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();



await builder.Build().RunAsync();