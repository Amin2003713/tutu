using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Banner.Interfaces;
using Application.Category.Interfaces;
using Application.Comment.Interfaces;
using Application.Common;
using Application.Order.Interfaces;
using Application.User.Auth.Interfaces;
using Application.User.Users.Interfaces;
using Blazored.LocalStorage;
using Infra.Banner.Implantations;
using Infra.Category.Implantations;
using Infra.Comment.Implantations;
using Infra.Common;
using Infra.Orders.Implantations;
using Infra.User.Auth.Implantations;
using Infra.User.Users.Implantations;
using Infra.Utils;
using Infra.UtilsService;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class InfraDi
{

    public static IServiceCollection RegisterInfraDependency(this IServiceCollection services)
    {
        services.AddApisToDiRegistry();


        services.AddBlazoredLocalStorage(config =>
        {
            config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            config.JsonSerializerOptions.WriteIndented = false;
        });

        return services;
    }


    private static IServiceCollection AddApisToDiRegistry(this IServiceCollection services)
    {
        const string baseAddress = "https://localhost:5001/";

        services.AddHttpClient<IBaseHttpClient, BaseHttpClient>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });

        services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILocalStorage  , LocalStorage >();

        

        

        return services;
    }
}