using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositriers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure;

public static class InfrastructureRegisteratition
{
    public static IServiceCollection InfrastructureConfiguration ( this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IGenericRepositry<>),typeof(GenericRepositry<>));
        // apply unit of work
        services.AddScoped<IUniteOfWork, UniteOfWork>();
        // applyDbContext
        services.AddDbContext<AppDbContext>(op =>
           op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"))
        );
        return services;
    }
}
