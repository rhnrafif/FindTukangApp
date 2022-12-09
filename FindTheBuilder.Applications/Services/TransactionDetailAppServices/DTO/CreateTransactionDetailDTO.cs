using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO
{
	public class CreateTransactionDetailDTO
	{
		public int TransactionId { get; set; }
		public string ProductName { get; set; }
		public int BuildingDay { get; set; }
	}
}
