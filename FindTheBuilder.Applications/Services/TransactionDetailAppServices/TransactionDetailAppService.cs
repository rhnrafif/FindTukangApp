using AutoMapper;
using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Databases;
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

		public TransactionDetailAppService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
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
