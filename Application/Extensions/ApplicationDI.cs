using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static  class ApplicationDi
{
    public static IServiceCollection RegisterApplicationDependency(this IServiceCollection services)
    {



        return services;
    }
}