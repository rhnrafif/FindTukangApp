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
		AuthDto Register(AuthDto model);
		AuthDto Login(AuthLoginDto model);
		AuthDto AutenticateUser (AuthDto model);
	}
}
