using AutoMapper;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TukangAppServices
{
	public class TukangAppService : ITukangAppService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public TukangAppService(AppDbContext context, IMapper mapper)
		{
			_context= context;
			_mapper= mapper;
		}
		public Tukang Create(TukangDTO model)
		{
			var tukang = _mapper.Map<Tukang>(model);
			_context.Tukang.Add(tukang);
			_context.SaveChanges();

			return tukang;
		}

		public Tukang Update(UpdateTukangDTO model)
		{
			var getTukang = GetByName(model.Name);

			if (getTukang.Id != 0) 
			{
				var tukang = _mapper.Map<Tukang>(model);
				getTukang.Id = tukang.Id;
				_context.Tukang.Update(tukang);
				_context.SaveChanges();

				return tukang;
			}
			
			return new Tukang() { Name = null };
		}

		private Tukang GetByName (string name)
		{
			var tukang = new Tukang();
			var getTukang = _context.Tukang.AsNoTracking().FirstOrDefault(x => x.Name == name);
			if (getTukang == null)
			{
				return tukang;
			}
			return tukang = getTukang;
		}
	}
}
