using AutoMapper;
using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Databases;
using FindTheBuilder.Databases.Models;
using Microsoft.VisualBasic;
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
		private readonly ITukangAppService _tukangAppService;

		public TransactionDetailAppService(AppDbContext context, IMapper mapper, IPriceAppService priceAppService, ITukangAppService tukangAppService)
		{
			_context = context;
			_mapper = mapper;
			_priceAppService = priceAppService;
			_tukangAppService = tukangAppService;
		}

		public async Task<TransactionDetails> CreateTransactionDetail(CreateTransactionDetailDTO model)
		{
			try
			{
				var result = new TransactionDetails();

				//get price
				var price = await _priceAppService.GetPriceById(model.PriceId);

				if (price.Price != 0)
				{

					var data = _mapper.Map<TransactionDetails>(model);
					data.DueDate = DateTime.Now.AddDays(model.BuildingDay);
					data.Total = price.Price;

					//autoupdate tukang avibility by timer
					//var timer = new PeriodicTimer(TimeSpan.FromSeconds(model.BuildingDay));
					//while(await timer.WaitForNextTickAsync())
					//{
					//	await _tukangAppService.UpdateAvailbility(price.TukangId);
					//}

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
						})
						//.Skip(pageInfo.Skip)
						.Take(pageInfo.PageSize)
						.OrderBy(w => w.DueDate),
				Total = _context.TransactionDetails.Count()
			};

			return await Task.Run(() => (result));
		}
	}
}
