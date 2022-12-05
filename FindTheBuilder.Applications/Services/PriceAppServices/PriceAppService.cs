using AutoMapper;
using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
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
			_context.Prices.Add(price);
			_context.SaveChanges();

			return price;
		}

		public Prices Delete(int id)
		{
			var price = _context.Prices.FirstOrDefault(x => x.Id == id);
			if (price != null)
			{
				_context.Prices.Remove(price);
				_context.SaveChanges();
			}

			return price;
		}

		public PagedResult<PriceListDTO> GetPriceByProduct(PageInfo pageInfo)
		{
			var pagedResult = new PagedResult<PriceListDTO>
			{
				Data = (
				from price in _context.Prices
				join tukang in _context.Tukang
				on price.TukangId equals tukang.Id
				join skill in _context.Skills
				on tukang.SkillId equals skill.Id
				join product in _context.Products
				on price.ProductId equals product.Id
				select new PriceListDTO
				{
					TukangName = tukang.Name,
					TukangSkill = skill.Name,
					TukangProducts = product.Type,
					Size = price.Size,
					Price = price.Price
				}).Skip(pageInfo.Skip)
				.Take(pageInfo.PageSize)
				.OrderBy(w => w.TukangProducts),
				Total = _context.Products.Count()
			};

			return pagedResult;
		}

		public Prices Update(UpdatePriceDTO model)
		{
			var price = _mapper.Map<Prices>(model);
			_context.Prices.Update(price);
			_context.SaveChanges();

			return price;
		}
	}
}
