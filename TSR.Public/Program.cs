using Application;
using Application.Interfaces;
using Microsoft.Extensions.FileProviders;
using TSR.Public.Injection;
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;
services.AddHttpContextAccessor();
services.AddControllersWithViews();
var url = config.GetValue<string>("API")!;
services.RegisterApplication(url);
var timeOut= config.GetValue<int>("SessionTimeOut")!;
services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(timeOut);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
services.AddLocalizationWithControllersAndView();
services.AddAuth();
services.AddMemoryCache();

var app = builder.Build();
app.UseStaticFiles();
var sharedFilesPath = config.GetValue<string>("ImagesPath")!;
if (Directory.Exists(sharedFilesPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(sharedFilesPath),
        RequestPath = "/SharedFiles"
    });
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UsePathBase("/TasierSooq");
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



using (var scope = app.Services.CreateScope())
{
    var brandService = scope.ServiceProvider.GetRequiredService<IBrandService>();
    await brandService.GetAllBrands(); 
}


app.Run();
