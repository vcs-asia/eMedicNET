using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using eMedicEntityModel.Models.v1;
using eMedicNETv7.Data;

namespace Stg.Web.v1.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	public class AccountController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger<AccountController> _logger;

		public AccountController(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
		{
			_configuration = configuration;
			_context = context;
			_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			_logger.LogInformation("User logged out.");
			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
		{
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.Email);
                }

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.ToString());
                    }
                }
            }
            ModelState.AddModelError("", string.Join(";", ModelState.SelectMany(k => k.Value!.Errors).Select(k => k.ErrorMessage)));
            return View(model);
        }
    }
}
