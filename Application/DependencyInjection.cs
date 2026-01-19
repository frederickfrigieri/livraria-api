using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<ILivroAppService, LivroAppService>();
        services.AddScoped<IAutorAppService, AutorAppService>();
        services.AddScoped<IAssuntoAppService, AssuntoAppService>();
        services.AddScoped<IFormaCompraAppService, FormaCompraAppService>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}