using Application.Extensions;
using MudBlazor.Services;
using BlazorProjetc.UI.Components;
using Infra.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();




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

builder.Services.RegisterApplicationDependency();
builder.Services.RegisterInfraDependency();
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

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["token"]?.ToString();

    if (!token.IsNullOrEmpty())
        context.Request.Headers.Append("authorization", $"Bearer {token}");

    await next();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorProjetc.UI.Client._Imports).Assembly);

app.UseAuthorization();


app.Run();