using Domain.Bases.Interfaces.Repositories;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Repositories;
using Infrastructure.Bases.Data;
using Infrastructure.Bases.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bases;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(@"Server=.;Database=CleanApi;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");
        });
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }
}
