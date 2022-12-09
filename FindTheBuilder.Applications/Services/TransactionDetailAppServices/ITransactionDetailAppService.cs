using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionDetailAppServices
{
	public interface ITransactionDetailAppService
	{
		PagedResult<TransactionDetailDTO> GetAllTransactions(PageInfo pageInfo);
		TransactionDetailDTO CreateTransactionDetail(CreateTransactionDetailDTO model);
	}
}
