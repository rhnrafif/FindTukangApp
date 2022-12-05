using AutoMapper;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
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
			var tukang = _mapper.Map<Tukang>(model);
			_context.Tukang.Update(tukang);
			_context.SaveChanges();

			return tukang;
		}
	}
}
