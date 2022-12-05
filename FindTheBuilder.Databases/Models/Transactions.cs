using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Databases.Models
{
	[Table("transaction")]
	public class Transactions
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int PriceId { get; set; }
		public DateTime TransactionDate { get; set; }
		public bool PaymentStatus { get; set; }

		public virtual Customers Customer { get; set; }
		public virtual Prices Price { get; set; }

		public virtual IEnumerable<TransactionDetails> TransactionDetails { get; set; }
	}
}
