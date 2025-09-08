using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TSR.Public.Helper;
using TSR.Public.Models.Account;

namespace TSR.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly CommonLocalizer _loc;
        public AccountController(IUserService userService, CommonLocalizer loc)
        {
            _userService = userService;
            _loc = loc;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.Login(model.Username, model.Password);
            if (result == null)
            {
                ModelState.AddModelError("", _loc["InvalidUsernameOrPassword"].Value);
                return View(model);
            }
            if (result == Application.Enums.EnumPublicUserStatus.Locked)
            {
                ModelState.AddModelError("", _loc["YourAccountIsLocked"].Value);
                return View(model);
            }
            if (result == Application.Enums.EnumPublicUserStatus.New)
            {
                ModelState.AddModelError("", _loc["YourAccountIsNotActivatedYet"].Value);
            }

            return RedirectToAction("OtpVerification", "Home", new { returnUrl = model.ReturnUrl });
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public ViewResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OtpVerification(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl; 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OtpVerification(string code, string? returnUrl)
        {
            var result=  await _userService.ValidateOtp(code);
            if (!result)
            {
                ViewData["ReturnUrl"] = returnUrl;
                ModelState.AddModelError(nameof(code), _loc["OtpIsNotCorrect"]);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
