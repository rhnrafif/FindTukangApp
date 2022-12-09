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
		Transactions Create(TransactionDTO model);
		Transactions Update(UpdateTransactionDTO model);
		Transactions UpdatePayment(int transId);
		ICollection<Transactions> GetTransActiveByName(string name);
	}
}
