using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Databases.Models
{
	[Table("transaction_detail")]
	public class TransactionDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int TransactionId { get; set; }
		public float Total { get; set; }
		public DateTime DueDate { get; set; }

		public virtual Transactions Transaction { get; set; }
	}
}
