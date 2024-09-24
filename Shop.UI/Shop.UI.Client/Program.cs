using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Extensions;
using Blazored.SessionStorage;
using Infra.Extensions;
using Infra.Utils;
using Shop.UI.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using MudBlazor;
using MudBlazor.Services;
using Shop.UI.Client.Common.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);





await builder.Build().RunAsync();