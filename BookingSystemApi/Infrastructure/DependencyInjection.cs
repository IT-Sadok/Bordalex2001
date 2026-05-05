using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.Interfaces;
using Application.Features.Bookings.Interfaces;
using Application.Features.Imports.Interfaces;
using Application.Features.Users.Interfaces;
using Application.Imports;
using Infrastructure.Auth;
using Infrastructure.Data;
using Infrastructure.Features.Users.Handlers;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Imports;
using Infrastructure.Imports.Processing;
using Infrastructure.Imports.Services;
using Infrastructure.Persistence.Seeders;
using Infrastructure.Persistence.Seeders.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.UserContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Data;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<IdentityOptionsConfiguration>(configuration.GetSection("Identity"));

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                    npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                });
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

        services.AddAuthorization();

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddSignInManager()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        var handlerInterfaceType = typeof(IRequestHandler<,>);
        var assembly = typeof(LoginUserHandler).Assembly;

        var handlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                .Select(i => new { HandlerType = t, InterfaceType = i }));

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.InterfaceType, handler.HandlerType);
        }

        services.AddScoped<IUserManagerWrapper<AppUser>, UserManagerWrapper>();
        services.AddScoped<IRoleManagerWrapper<IdentityRole>, RoleManagerWrapper>();
        services.AddScoped<ISignInManagerWrapper<AppUser>, SignInManagerWrapper>();

        services.AddScoped<IDbConnection>(sp =>
        {
            var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            return connection;
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext.UserContext>();
        services.AddScoped<IInitialDbSeeder, InitialDbSeeder>();
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IImportJobRepository, ImportJobRepository>();
        services.AddScoped<IJsonBatchReader, JsonBatchReader>();
        services.AddScoped<IImportBatchProcessor, ImportBatchProcessor>();
        services.AddScoped<IImportStorage, FileSystemImportStorage>();
        services.AddScoped<IDataImportService, DataImportService>();

        services.AddHostedService<ImportBackgroundService>();

        return services;
    }
}
