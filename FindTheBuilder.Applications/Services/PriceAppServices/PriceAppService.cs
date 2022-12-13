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

		public Prices Create(PriceDTO model)
		{
			var price = _mapper.Map<Prices>(model);

			price.IsDeleted = false;
			_context.Prices.Add(price);
			_context.SaveChanges();

			return price;
		}

		public Prices Delete(string product)
		{
			var price = _context.Prices.AsNoTracking().FirstOrDefault(x => x.Product == product);
			if (price != null)
			{
				price.IsDeleted = true;
				_context.Prices.Update(price);
				_context.SaveChanges();
			}

			return price;
		}

		public Prices GetPriceById(int id)
		{
			var price = _context.Prices.FirstOrDefault(w => w.Id == id);
			if (price != null)
			{
				return price;
			}
			return price;
		}

		public Prices Update(UpdatePriceDTO model)
		{
			var getPrice = GetByProduct(model.Product);
			if (getPrice.Id != 0)
			{
				var price = _mapper.Map<Prices>(model);
				getPrice.IsDeleted = false;
				_context.Prices.Update(price);
				_context.SaveChanges();

				return price;
			}

			return new Prices();
		}

		public Prices GetByProduct(string product)
		{
			var price = new Prices();
			var getPrice = _context.Prices.AsNoTracking().FirstOrDefault(x => x.Product == product);
			if (getPrice == null)
			{
				return price;
			}

			return price = getPrice;
		}

		public PagedResult<AllPriceListDTO> GetAllPrice(PageInfo pageInfo)
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

			return pagedResult;
		}
	}
}
