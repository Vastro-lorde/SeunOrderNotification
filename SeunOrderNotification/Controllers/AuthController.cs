using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SeunOrderNotification.Models;
using SeunOrderNotification.Services;
using Microsoft.AspNetCore.Identity.Data;
using System.Reflection;

namespace SeunOrderNotification.Controllers
{
	public class AuthController : Controller
	{
		private readonly ILogger<AuthController> _logger;
		private readonly IAuthService _authService;
		private readonly IJwtService _jwtService;


		public AuthController(ILogger<AuthController> logger, IAuthService authService, IJwtService jwtService)
		{
			_logger = logger;
			_authService = authService;
			_jwtService = jwtService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel request)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Index", "Auth", request);
			}
			var user = await _authService.LoginAsync(request.Email, request.Password);

			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt");
				return View(request);
			}
			/*if (user == null) {
			}*/

			// Generate JWT token
			var token = _jwtService.GenerateToken(user);

			// Store token in HttpContext for further use
			HttpContext.Session.SetString("JwtToken", token);
			HttpContext.Session.SetString("UserId", user.Id);
			HttpContext.Session.SetString("UserEmail", user.Email);

			// Sign in the user
			var claims = new[] {
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

			// Redirect to dashboard
			return RedirectToAction("Index", "Dashboard");

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet]
		public IActionResult Logout()
		{
			// Clear the session
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
