using AutoMapper;
using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.PriceAppServices
{
	public class PriceAppService : IPriceAppService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public PriceAppService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Prices> Create(PriceDTO model)
		{
			var price = _mapper.Map<Prices>(model);

			price.IsDeleted = false;
			await _context.Prices.AddAsync(price);
			await _context.SaveChangesAsync();

			return await Task.Run(()=>(price));
		}

		public  async Task<Prices> Delete(string product)
		{
			var price = await _context.Prices.AsNoTracking().FirstOrDefaultAsync(x => x.Product == product);
			if (price != null)
			{
				price.IsDeleted = true;
				_context.Prices.Update(price);
				await _context.SaveChangesAsync();
			}

			return await Task.Run(()=>(price));
		}

		public async Task<Prices> GetPriceById(int id)
		{
			var price = await _context.Prices.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
			if (price != null)
			{
				return await Task.Run(() => (price));
			}
			return await Task.Run(() => (price));
		}

		public async Task<Prices> Update(UpdatePriceDTO model)
		{
			var getPrice = await GetPriceById(model.Id);
			if (getPrice != null)
			{
				var price = _mapper.Map<Prices>(model);
				price.IsDeleted = false;
				price.Id = getPrice.Id;
				price.ImagePath = getPrice.ImagePath;
				_context.Prices.Update(price);
				await _context.SaveChangesAsync();

				return await Task.Run(()=>(price));
			}

			return await Task.Run(()=>(new Prices()));
		}

		public async Task<Prices> GetByProduct(string product)
		{
			var price = new Prices();
			var getPrice = await _context.Prices.AsNoTracking().FirstOrDefaultAsync(x => x.Product == product);
			if (getPrice == null)
			{
				return await Task.Run(()=>(price));
			}

			return await Task.Run(()=>(price = getPrice));
		}

		public async Task<PagedResult<AllPriceListDTO>> GetAllPrice(PageInfo pageInfo)
		{
			var pagedResult = new PagedResult<AllPriceListDTO>
			{
				Data = (from price in _context.Prices
						join tukang in _context.Tukang
						on price.TukangId equals tukang.Id
						join skill in _context.Skills
						on price.SkillId equals skill.Id
						where price.IsDeleted == false
						select new AllPriceListDTO
						{
							TukangName = tukang.Name, 
							TukangSkill = skill.Name,
							TukangProducts = price.Product,
							Size = price.Size,
							Price = price.Price
						})
						.Skip(pageInfo.Skip)
						.Take(pageInfo.PageSize)
						.OrderBy(w => w.TukangSkill),
				Total = _context.Prices.Where(w => w.IsDeleted == false).Count()
			};

			return await Task.Run(()=>(pagedResult));
		}

		public async Task SaveImage(PriceDTO model)
		{
			var uniqueFileName = FileHelper.GetUniqueFileName(model.Image.FileName);
			var upload = Path.Combine("user", "post", model.TukangId.ToString());
			var filePath = Path.Combine(upload, uniqueFileName);

			Directory.CreateDirectory(Path.GetDirectoryName(filePath));

			await model.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
			model.ImagePath = filePath;
			return;
		}

		public async Task<Prices> DownloadImage(int id)
		{
			var imageFile = await _context.Prices.FirstOrDefaultAsync(w => w.Id == id);
			return imageFile;
		}
	}
}
