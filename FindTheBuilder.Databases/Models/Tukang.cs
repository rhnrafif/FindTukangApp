using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Databases.Models
{
	[Table("tukang")]
	public class Tukang
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }

		public virtual IEnumerable<Prices> Prices { get; set; }
	}
}
