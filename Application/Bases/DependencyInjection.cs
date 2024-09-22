using Application.Bases.Implements.Services;
using Application.Bases.Interfaces.IServices;
using Application.IServices;
using Application.Services;
using Domain.Bases.Interfaces.Repositories;
using Infrastructure.Bases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Bases;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
