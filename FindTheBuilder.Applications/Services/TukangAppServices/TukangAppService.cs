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
		public async Task<Tukang> Create(TukangDTO model)
		{
			var tukang = _mapper.Map<Tukang>(model);
			tukang.IsAvailable = true;
			await _context.Tukang.AddAsync(tukang);
			await _context.SaveChangesAsync();

			return await Task.Run(()=>(tukang));
		}

		public async Task<Tukang> Update(UpdateTukangDTO model)
		{
			var getTukang = await GetById(model.Id);

			if (getTukang.Id != 0) 
			{
				var tukang = _mapper.Map<Tukang>(model);
				getTukang.Id = tukang.Id;
				getTukang.IsAvailable = true;
				_context.Tukang.Update(tukang);
				await _context.SaveChangesAsync();

				return await Task.Run(()=>(tukang));
			}
			
			return await Task.Run(()=>(new Tukang() { Name = null }));
		}

		private async Task<Tukang> GetByName (string name)
		{
			var tukang = new Tukang();
			var getTukang = await _context.Tukang.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
			if (getTukang == null)
			{
				return await Task.Run(()=>(tukang));
			}
			return await Task.Run(()=>(tukang = getTukang));
		}

		public async Task<Tukang> GetById (int id)
		{
			var tukang = new Tukang();
			var getTukang = await _context.Tukang.AsNoTracking().Where(w => w.IsAvailable == true).FirstOrDefaultAsync(x => x.Id == id);
			if (getTukang == null)
			{
				return await Task.Run(() => (tukang));
			}
			return await Task.Run(() => (tukang = getTukang));
		}

		public async Task<bool> UpdateAvailbility(int tukangId)
		{
			try
			{			

				var dataTukang = await _context.Tukang.FirstOrDefaultAsync(w => w.Id == tukangId);
				if(dataTukang != null)
				{
					dataTukang.IsAvailable = false;
					_context.Tukang.Update(dataTukang);
					await _context.SaveChangesAsync();

					return await Task.Run(() => (true));
				}
				return await Task.Run(() => (false));
			}
			catch
			{
				return await Task.Run(() => (false));
			}
		}
	}
}
