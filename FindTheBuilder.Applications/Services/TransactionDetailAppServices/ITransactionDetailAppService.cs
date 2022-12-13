using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionDetailAppServices
{
	public interface ITransactionDetailAppService
	{
		Task<PagedResult<TransactionDetailDTO>> GetAllTransactions(PageInfo pageInfo);
		Task<TransactionDetails> CreateTransactionDetail(CreateTransactionDetailDTO model);
	}
}
