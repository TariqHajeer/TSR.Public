using System.Diagnostics;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TSR.Public.Models;

namespace TSR.Public.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBrandService _brandService;
    public HomeController(ILogger<HomeController> logger, IBrandService brandService)
    {
        _logger = logger;
        _brandService = brandService;
    }

    public async Task<IActionResult> Index()
    {
        var data= await _brandService.GetAllBrands();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
