using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.AuthAppServices;
using FindTheBuilder.Applications.Services.AuthAppServices.Dto;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
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
		private ITukangAppService _tukangAppService;
		public AuthController(IAuthAppService authAppService, IConfiguration configuration, ITukangAppService tukangAppService)
		{
			_authAppService = authAppService;
			_configuration = configuration;
			_tukangAppService = tukangAppService;
		}

		[HttpPost("login")]
		[EnableCors("AllowAllHeadersPolicy")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] AuthLoginDto model)
		{
			try
			{				
				var user = await _authAppService.Login(model);
				var userData = await _authAppService.AutenticateUser(user);
				if(userData.Name != null)
				{
					string role = null;

					if (userData.Role == 1)
					{
						role = "Tukang";
					}
					else if (userData.Role == 2)
					{
						role = "Customer";
					}

					var jwt = await GenerateJSONWebToken(userData, role);
					//response = Ok(new { token = jwt });

					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), Ok(new { token = jwt }), "Success")));
				}

				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "User Not Found")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] AuthDto model)
		{
			try
			{
				var userData = new AuthDto();
				switch (model.Role)
				{
					case 1:
						userData = await _authAppService.Register(model);
						break;
					case 2:
						userData = await _authAppService.Register(model);
						break;
					default:
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Input Not Valid")));
						break;
				}

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

					var jwt = await GenerateJSONWebToken(userData, role);
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), Ok(new { token = jwt }), "Success")));
				}

				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Input Error")));
			}
			catch  (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}



		private async Task<string> GenerateJSONWebToken(AuthDto model, string role)
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
			
			return await Task.Run(()=>(new JwtSecurityTokenHandler().WriteToken(token)));
		}

	}
}
