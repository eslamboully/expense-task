using System.Text.Json.Serialization;
using Infrastructure.Interfaces;
using Infrastructure;
using Infrastructure.Services;

namespace WEB.Extensions;
public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddControllersWithViews().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });


        return services;
    }

}