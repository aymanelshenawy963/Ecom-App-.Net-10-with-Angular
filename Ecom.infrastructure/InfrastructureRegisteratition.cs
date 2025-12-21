using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositriers;
using Ecom.infrastructure.Repositriers.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
        services.AddScoped<IUnitOfWork, UniteOfWork>();
        services.AddSingleton<IImageMangementService, ImageMangementService>();
        services.AddSingleton<IFileProvider>
            (new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") ));
        // applyDbContext
        services.AddDbContext<AppDbContext>(op =>
           op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"))
        );
        return services;
    }
}
