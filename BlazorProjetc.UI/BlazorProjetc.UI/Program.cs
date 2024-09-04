using System.Net.Http.Headers;
using Application.Extensions;
using MudBlazor.Services;
using BlazorProjetc.UI.Components;
using BlazorProjetc.UI.Services;
using BlazorProjetc.UI.Services.Auth;
using Infra.Common;
using Infra.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;

const string baseAddress = "https://localhost:5001/";

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();


// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopLeft;

    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2;
    config.SnackbarConfiguration.HideTransitionDuration = 5;
    config.SnackbarConfiguration.ShowTransitionDuration = 5;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;

    config.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
    config.SnackbarConfiguration.ClearAfterNavigation = true;
    config.SnackbarConfiguration.BackgroundBlurred = true;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddScoped<LocalStorage>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = "Eshop.com",
        ValidAudience = "Eshop-api",
        IssuerSigningKey =
            new SymmetricSecurityKey("16D9BBF8-FA00-4D89-9BB5-99610E95BdsadasdasdA70-dasdsa"u8.ToArray()),
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorProjetc.UI.Client._Imports).Assembly);

app.UseAuthorization();


app.Run();