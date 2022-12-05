using FindTheBuilder.Applications.Services.AuthAppServices;
using FindTheBuilder.Applications.Services.AuthAppServices.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindTheBuilder.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private IAuthAppService _authAppService;
		private IConfiguration _configuration;
		public AuthController(IAuthAppService authAppService, IConfiguration configuration)
		{
			_authAppService = authAppService;
			_configuration = configuration;
		}

		[HttpGet("login")]
		[AllowAnonymous]
		public IActionResult Login([FromQuery] AuthDto model)
		{
			IActionResult response = Unauthorized();
			try
			{
				
				var user = _authAppService.Login(model);
				var userData = _authAppService.AutenticateUser(user);
				if(userData != null)
				{
					string role = null;

					if (model.Role == 1)
					{
						role = "Tukang";
					}
					else if (model.Role == 2)
					{
						role = "Customer";
					}

					var jwt = GenerateJSONWebToken(model, role);
					response = Ok(new { token = jwt });
				}

				return response;
			}
			catch
			{
				return response;
			}
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public IActionResult Register([FromBody] AuthDto model)
		{
			IActionResult response = Unauthorized();
			try
			{
				
				var userData = _authAppService.Register(model);

				if(userData != null)
				{
					string role = null;

					if(model.Role == 1)
					{
						role = "Tukang";
					}
					else if(model.Role == 2)
					{
						role = "Customer";
					}

					var jwt = GenerateJSONWebToken(userData, role);
					response = Ok(new { token = jwt });
				}

				return response;
			}
			catch
			{
				return response;
			}
		}

		[HttpGet("trial")]
		[Authorize(Roles = "Tukang")]
		public IActionResult Trial(string model)
		{
			IActionResult response = null;
			var result = _authAppService.Trial(model);

			if(result != null)
			{
				response = Ok(new { res = "Oke" });
			}
			return response;
		}

		private string GenerateJSONWebToken(AuthDto model, string role)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); //jwt key mengarah ke config appsettings
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims: new[]
						{							
							new Claim(ClaimTypes.Role, role)
						},
						expires: DateTime.Now.AddMinutes(1200),
						signingCredentials: credentials
						) ;
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
