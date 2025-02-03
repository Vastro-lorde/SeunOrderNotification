using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SeunOrderNotification.Attributes
{
	public class JwtAuthorizeAttribute : TypeFilterAttribute
	{
		public JwtAuthorizeAttribute() : base(typeof(JwtAuthorizeFilter))
		{
		}
	}

	public class JwtAuthorizeFilter : IAuthorizationFilter
	{
		private readonly IConfiguration _configuration;

		public JwtAuthorizeFilter(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			// Check if the user is authenticated
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			// Get the JWT token from the session
			var token = context.HttpContext.Session.GetString("JwtToken");

			if (string.IsNullOrEmpty(token))
			{
				context.Result = new RedirectToActionResult("Login", "Auth", null);
				return;
			}

			try
			{
				// Validate the token
				ValidateToken(token, context);
			}
			catch (Exception)
			{
				// If token validation fails, redirect to login
				context.Result = new RedirectToActionResult("Login", "Auth", null);
			}
		}

		private void ValidateToken(string token, AuthorizationFilterContext context)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidIssuer = _configuration["Jwt:Issuer"],
				ValidateAudience = true,
				ValidAudience = _configuration["Jwt:Audience"],
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			}, out SecurityToken validatedToken);
		}
	}
}