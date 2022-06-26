namespace WEB.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication()
            .AddCookie("user", config => 
            {
                config.LoginPath = "/account/login";
                config.LogoutPath = "/account/logout";
                config.AccessDeniedPath = "/error";
            });

        return services;
    }
}