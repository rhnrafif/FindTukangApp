using FindTheBuilder.Applications.Services.AuthAppServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.AuthAppServices
{
	public interface IAuthAppService
	{
		Task <AuthDto> Register(AuthDto model);
		Task<AuthDto> Login(AuthLoginDto model);
		Task<AuthDto> AutenticateUser (AuthDto model);
	}
}
