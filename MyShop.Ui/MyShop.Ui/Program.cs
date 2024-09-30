using Application.Extensions;
using Domain.User.Auth;
using Infra.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using MyShop.Ui.AuthenticationProvider;
using MyShop.Ui.Client.Pages;
using MyShop.Ui.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCascadingAuthenticationState();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddAuthentication(AuthConfig.ShopSchema)
    .AddCookie(AuthConfig.ShopSchema, options =>
    {
        options.Cookie.Name = AuthConfig.AuthCookie;
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth/access-denied";
        options.LogoutPath = "/auth/logout";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthStateProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.RegisterInfraDependency();
builder.Services.RegisterApplicationDependency();

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

app.UseAuthentication()
    .UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MyShop.Ui.Client._Imports).Assembly);

app.Run();