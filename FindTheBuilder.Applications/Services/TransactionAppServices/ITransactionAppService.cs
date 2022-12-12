using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionAppServices
{
	public interface ITransactionAppService
	{
		Task<Transactions> Create(TransactionDTO model);
		Task<Transactions> Update(UpdateTransactionDTO model);
		Task<Transactions> UpdatePayment(int transId);
		Task<ICollection<Transactions>> GetTransActiveByName(string name);
	}
}
