using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TSR.Public.Injection;

public static class Authentication
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Redirect here if not authenticated
            options.AccessDeniedPath = "/Account/AccessDenied"; // Custom access denied page
            options.ReturnUrlParameter = "returnUrl";
        });
        return services;
    }
}
