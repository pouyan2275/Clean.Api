using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
}
