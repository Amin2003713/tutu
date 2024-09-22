using Application.Extensions;
using Infra.Extensions;
using MyShopUi.Client.Pages;
using MyShopUi.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.RegisterApplicationDependency();
builder.Services.RegisterInfraDependency();

const string authScheme = "Shop-Auth";
const string cookieName = "Auth-Cookie";
builder.Services.AddCascadingAuthenticationState(); 

builder.Services.AddAuthentication()
    .AddCookie(authScheme , options =>
    {
        options.Cookie.Name = cookieName;
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.LogoutPath = "/Auth/Logout";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
    } );


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
    .AddAdditionalAssemblies(typeof(MyShopUi.Client._Imports).Assembly);

app.Run();