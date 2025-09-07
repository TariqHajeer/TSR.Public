using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services, string baseUrl)
    {
        services.AddScoped<IHttpContextService, HttpContextService>();
        services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});
        services.AddScoped<IBrandService, BrandService>();

        return services;
    }
}
