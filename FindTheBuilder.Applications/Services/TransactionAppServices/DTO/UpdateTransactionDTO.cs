using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionAppServices.DTO
{
	public class UpdateTransactionDTO
	{
		public int Id { get; set; }
		public string CustomerId { get; set; }
		public int PriceId { get; set; }
	}
}
