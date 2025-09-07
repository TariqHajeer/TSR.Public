using Microsoft.AspNetCore.Mvc;

namespace TSR.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        public ViewResult Login()
        {
            return View();
        }
        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}
