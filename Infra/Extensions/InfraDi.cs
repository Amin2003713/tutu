using Application.Banner.Interfaces;
using Application.Category.Interfaces;
using Application.Common;
using Application.Users.Auth.Interfaces;
using Infra.Banner.Implantations;
using Infra.Category.Implantations;
using Infra.Common;
using Infra.Users.Auth.Implantations;
using Microsoft.Extensions.DependencyInjection;


namespace Infra.Extensions;

public static  class InfraDi
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

        services.AddScoped<IBaseHttpClient, BaseHttpClient>();
        services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddHttpClient<BaseHttpClient>(client => { client.BaseAddress = new Uri(baseAddress); });
       

        return services;
    }
}