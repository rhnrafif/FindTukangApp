using AutoMapper;
using BCrypt.Net;
using FindTheBuilder.Applications.Services.AuthAppServices.Dto;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
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

		public async Task<AuthDto> Register(AuthDto model)
		{
			try
			{
				var result = new AuthDto();				

				//Hashing
				model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

				var userData = _mapper.Map<Auth>(model);

				await _context.Database.BeginTransactionAsync();
				await _context.auths.AddAsync(userData);
				await _context.SaveChangesAsync();
				await _context.Database.CommitTransactionAsync();

				if(userData != null)
				{
					return await Task.Run(() => (result = _mapper.Map<AuthDto>(userData)));
				}
				
				return await Task.Run(() => (result));
								
			}
			catch
			{				
				await _context.Database.RollbackTransactionAsync();
				return await Task.Run(() => (new AuthDto() { Name = null, Password = null, Role = 0}));
			}
		}

		public async Task<AuthDto> Login(AuthLoginDto model)
		{
			try
			{
				var result = new AuthDto();
				var user = await _context.auths.FirstOrDefaultAsync(w => w.Name == model.Name);

				if(user == null)
				{
					return await Task.Run(()=>(result));
				}			

				//DeCrypt
				bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

				if (user != null && isValidPassword)
				{	
					return await Task.Run(()=>(result = _mapper.Map<AuthDto>(user)));
				}

				return await Task.Run(() => (result));
			}
			catch
			{
				return await Task.Run(() => (new AuthDto() { Name = null, Password = null, Role = 0 }));
			}
		}

		public async Task<AuthDto> AutenticateUser(AuthDto model)
		{
			AuthDto user = null;

			if (model != null)
			{
				user = new AuthDto { Name = model.Name, Password = model.Password, Role = model.Role };
			}

			return await Task.Run(()=>(user));
		}

	}
}
