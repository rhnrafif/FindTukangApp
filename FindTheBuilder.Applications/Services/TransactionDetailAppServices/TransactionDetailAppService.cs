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

		public async Task<TransactionDetails> CreateTransactionDetail(CreateTransactionDetailDTO model)
		{
			try
			{
				var result = new TransactionDetails();

				//get price
				var price = await _priceAppService.GetPriceById(model.PriceId);

				if (price.Price != null)
				{

					var data = _mapper.Map<TransactionDetails>(model);
					data.DueDate = DateTime.Now.AddDays(model.BuildingDay);
					data.Total = price.Price;

					await _context.Database.BeginTransactionAsync();
					await _context.TransactionDetails.AddAsync(data);
					await _context.SaveChangesAsync();
					await _context.Database.CommitTransactionAsync();

					return await Task.Run(()=>(result = _mapper.Map<TransactionDetails>(data)));
				}

				return await Task.Run(()=>(result));
			}
			catch
			{
				await _context.Database.RollbackTransactionAsync();
				return await Task.Run(()=>( new TransactionDetails()));
			}
		}

		public async Task<PagedResult<TransactionDetailDTO>> GetAllTransactions(PageInfo pageInfo)
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

			return await Task.Run(() => (result));
		}
	}
}
