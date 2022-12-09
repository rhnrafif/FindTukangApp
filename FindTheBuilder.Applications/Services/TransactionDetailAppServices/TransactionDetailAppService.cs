using AutoMapper;
using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionDetailAppServices
{
	public class TransactionDetailAppService : ITransactionDetailAppService
	{
		private AppDbContext _context;
		private IMapper _mapper;
		private readonly IPriceAppService _priceAppService;

		public TransactionDetailAppService(AppDbContext context, IMapper mapper, IPriceAppService priceAppService)
		{
			_context = context;
			_mapper = mapper;
			_priceAppService = priceAppService;
		}

		public TransactionDetailDTO CreateTransactionDetail(CreateTransactionDetailDTO model)
		{
			try
			{
				var result = new TransactionDetailDTO();

				//get price
				var price = _priceAppService.GetByProduct(model.ProductName);

				if (price.Price != null)
				{

					var data = _mapper.Map<TransactionDetails>(model);
					data.DueDate = DateTime.Now.AddDays(model.BuildingDay);
					data.Total = price.Price;

					_context.Database.BeginTransaction();
					_context.TransactionDetails.Add(data);
					_context.SaveChanges();
					_context.Database.CommitTransaction();

					return result = _mapper.Map<TransactionDetailDTO>(data);
				}

				return result;
			}
			catch
			{
				_context.Database.RollbackTransaction();
				return new TransactionDetailDTO();
			}
		}

		public PagedResult<TransactionDetailDTO> GetAllTransactions(PageInfo pageInfo)
		{
			var result = new PagedResult<TransactionDetailDTO>
			{
				Data = (from details in _context.TransactionDetails
						select new TransactionDetailDTO
						{
							Id = details.Id,
							TransactionId= details.TransactionId,
							DueDate= details.DueDate,
							Total = details.Total
						}).Skip(pageInfo.Skip)
						.Take(pageInfo.PageSize)
						.OrderBy(w => w.DueDate),
				Total = _context.TransactionDetails.Count()
			};

			return result;
		}
	}
}
