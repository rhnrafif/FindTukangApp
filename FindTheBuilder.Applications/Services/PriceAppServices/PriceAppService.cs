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

		public PagedResult<PriceListDTO> GetPriceByProduct(PageInfo pageInfo)
		{
			var pagedResult = new PagedResult<PriceListDTO>
			{
				Data = (from tukang in _context.Tukang
						join price in _context.Prices
						on tukang.Id equals price.TukangId
						join skill in _context.Skills
						on price.SkillId equals skill.Id
						select new PriceListDTO
						{
							TukangName = tukang.Name,
							TukangSkill = skill.Name,
							TukangProducts = price.Product,
							Size = price.Size,
							Price = price.Price
						}).Where(w => w.IsDeleted == false)
						.Skip(pageInfo.Skip)
						.Take(pageInfo.PageSize)
						.OrderBy(w => w.TukangSkill),
						Total = _context.Prices.Count()
			};

			return pagedResult;
		}

		public PriceDTO GetPriceByProductId(int productId)
		{
			var prices = new PriceDTO();
			var price = _context.Prices.FirstOrDefault(w => w.ProductId == productId);
			if (price != null)
			{
				prices = _mapper.Map<PriceDTO>(price);
			}
			return prices;
		}

		public Prices Update(UpdatePriceDTO model)
		{
			var getPrice = GetByProduct(model.Product);
			if (getPrice.Id != 0)
			{
				var price = _mapper.Map<Prices>(model);
				getPrice.IsDeleted= false;
				_context.Prices.Update(price);
				_context.SaveChanges();

				return price;
			}

			return new Prices();
		}

		private Prices GetByProduct(string product)
		{
			var price = new Prices();
			var getPrice = _context.Prices.AsNoTracking().FirstOrDefault(x => x.Product == product);
			if (getPrice == null)
			{
				return price;
			}

			return price = getPrice;
		}
	}
}
