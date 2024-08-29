using Application.Banner.Interfaces;
using Application.Category.Interfaces;
using Application.Comment.Interfaces;
using Application.Common;
using Application.Order.Interfaces;
using Application.User.Auth.Interfaces;
using Infra.Banner.Implantations;
using Infra.Category.Implantations;
using Infra.Comment.Implantations;
using Infra.Common;
using Infra.Orders.Implantations;
using Infra.User.Auth.Implantations;
using Infra.UtilsService;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class InfraDi
{
    public static IServiceCollection RegisterInfraDependency(this IServiceCollection services)
    {
        services.AddApisToDiRegistry();


        return services;
    }


    public static IServiceCollection AddApisToDiRegistry(this IServiceCollection services)
    {
        const string baseAddress = "https://localhost:5001/";

        services.AddHttpClient();
        services.AddScoped<LocalStorage>();
        services.AddScoped<IBaseHttpClient, BaseHttpClient>();
        services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddHttpClient<BaseHttpClient>(client => { client.BaseAddress = new Uri(baseAddress); });


        return services;
    }
}