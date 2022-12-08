using AutoMapper;
using BCrypt.Net;
using FindTheBuilder.Applications.Services.AuthAppServices.Dto;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.AuthAppServices
{
	public class AuthAppService : IAuthAppService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public AuthAppService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public AuthDto Register(AuthDto model)
		{
			try
			{
				var result = new AuthDto();				

				//Hashing
				model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

				var userData = _mapper.Map<Auth>(model);

				_context.Database.BeginTransaction();
				_context.auths.Add(userData);
				_context.SaveChanges();
				_context.Database.CommitTransaction();

				if(userData != null)
				{
					return result = _mapper.Map<AuthDto>(userData);
				}
				
				return result;
								
			}
			catch
			{				
				_context.Database.RollbackTransaction();
				return new AuthDto() { Name = null, Password = null, Role = 0};
			}
		}

		public AuthDto Login(AuthLoginDto model)
		{
			try
			{
				var result = new AuthDto();
				var user = _context.auths.FirstOrDefault(w => w.Name == model.Name);

				if(user == null)
				{
					return result;
				}			

				//DeCrypt
				bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

				if (user != null && isValidPassword)
				{	
					return result = _mapper.Map<AuthDto>(user);
				}

				return result;
			}
			catch
			{
				return new AuthDto() { Name = null, Password = null, Role = 0 };
			}
		}

		public AuthDto AutenticateUser(AuthDto model)
		{
			AuthDto user = null;

			if (model != null)
			{
				user = new AuthDto { Name = model.Name, Password = model.Password, Role = model.Role };
			}

			return user;
		}

	}
}
