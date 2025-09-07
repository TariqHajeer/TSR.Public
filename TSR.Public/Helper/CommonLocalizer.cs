using System;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace TSR.Public.Helper;

public class CommonLocalizer
{
    private readonly IStringLocalizer _localizer;
    public LocalizedString this[string key] => _localizer[key];
    public CommonLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Shared.Common", Assembly.GetExecutingAssembly().GetName().Name!);
    }
}
