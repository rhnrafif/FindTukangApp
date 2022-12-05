using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO
{
	public class TransactionDetailDTO
	{
		public int Id { get; set; }
		public int TransactionId { get; set; }
		public float Total { get; set; }
		public DateTime DueDate { get; set; }
	}
}
