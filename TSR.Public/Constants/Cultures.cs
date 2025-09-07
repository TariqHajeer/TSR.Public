using System;
using System.Globalization;

namespace TSR.Public.Constants;

public static class Cultures
{
    public const string AR = "ar";
    public const string EN = "en";

    public static readonly CultureInfo[] SupportedCultures = new[] { new CultureInfo(AR), new CultureInfo(EN) };
}
