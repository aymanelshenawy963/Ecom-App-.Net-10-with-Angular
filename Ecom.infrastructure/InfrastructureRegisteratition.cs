using Ecom.Core.Entites;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositriers;
using Ecom.infrastructure.Repositriers.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Ecom.infrastructure;

public static class InfrastructureRegisteratition
{
    public static IServiceCollection InfrastructureConfiguration ( this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IGenericRepositry<>),typeof(GenericRepositry<>));
        services.AddScoped<IUnitOfWork, UniteOfWork>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IGenerateToken, GenerateToken>();
        services.AddScoped<IAuth, AuthRepository>();


        services.AddSingleton<IConnectionMultiplexer>(i => {
            var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("redis")!);
            return ConnectionMultiplexer.Connect(configurationOptions);
        });

        services.AddSingleton<IImageMangementService, ImageMangementService>();
        services.AddSingleton<IFileProvider>
            (new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") ));
        // applyDbContext
        services.AddDbContext<AppDbContext>(op =>
           op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"))
        );

        services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        services.AddAuthentication(op =>    
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(o =>
        {
            o.Cookie.Name = "token";
            o.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };

        }).AddJwtBearer(op =>
        {
            op.RequireHttpsMetadata = false;
            op.SaveToken = true;
            op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]!)),
                ValidateIssuer = true,
                ValidIssuer = configuration["Token:Issuer"],
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero   
            };
            op.Events = new JwtBearerEvents()
            {
               OnMessageReceived = context =>
               {
                   var token = context.Request.Cookies["token"];
                   if (!string.IsNullOrEmpty(token)) 
                   {
                       context.Token = token;
                   }
                   return Task.CompletedTask;
               }
            };
        });
        return services;
    }
}
