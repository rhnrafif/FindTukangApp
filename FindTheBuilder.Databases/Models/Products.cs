using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Databases.Models
{
	[Table("product")]
	public class Products
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Type { get; set; }

		public virtual IEnumerable<Skills> Skill { get; set; }
		public virtual IEnumerable<Prices> Prices { get; set; }

	}
}
