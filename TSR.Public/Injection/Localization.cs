using System;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using TSR.Public.Constants;
using TSR.Public.Helper;

namespace TSR.Public.Injection;

public static class Localization
{
    public static IMvcBuilder AddLocalization(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            return factory.Create("Shared.Validation", Assembly.GetExecutingAssembly().GetName().Name!);
        };
    });
        return mvcBuilder;
    }
    public static IServiceCollection AddLocalizationWithControllersAndView(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddControllersWithViews().AddLocalization().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // map nameAr -> NameAr
                options.JsonSerializerOptions.PropertyNamingPolicy = null;          // keep PascalCase
            });
        ;
        services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new RequestCulture(Cultures.AR, Cultures.AR);
        options.SupportedCultures = Cultures.SupportedCultures;
        options.SupportedUICultures = Cultures.SupportedCultures;

        // Custom provider handles query + headers
        options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
        {
            var queryCulture = context.Request.Query["culture"].ToString();
            var userLangs = context.Request.Headers["Accept-Language"].ToString();

            var culture = !string.IsNullOrWhiteSpace(queryCulture)
                ? queryCulture
                : userLangs;

            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    var ci = new CultureInfo(culture);

                    if (ci.TwoLetterISOLanguageName == Cultures.EN)
                        return Task.FromResult(new ProviderCultureResult(Cultures.EN, Cultures.EN));
                    if (ci.TwoLetterISOLanguageName == Cultures.AR)
                        return Task.FromResult(new ProviderCultureResult(Cultures.AR, Cultures.AR));
                }
                catch (CultureNotFoundException)
                {
                    // ignore and fallback
                }
            }

            return Task.FromResult(new ProviderCultureResult(Cultures.AR, Cultures.AR));
        }));
    });

        services.AddSingleton<CommonLocalizer>();
        return services;
    }
}
