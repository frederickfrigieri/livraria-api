
using Domain.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Databases.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configuração do DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        // 2. Registro dos Repositórios
        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        services.AddScoped<IFormaCompraRepository, FormaCompraRepository>();

        return services;
    }
}