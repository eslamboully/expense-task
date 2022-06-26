using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WEB.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DatabaseContext>(c => c.UseSqlite(config.GetConnectionString("default")));
        return services;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        
        context.Database.Migrate();
        DatabaseContextSeed.SeedData(context);

        return app;
    }
}