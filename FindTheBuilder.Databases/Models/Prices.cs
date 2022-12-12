using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Databases.Models
{
	[Table("price")]
	public class Prices
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int TukangId { get; set; }
		public int SkillId { get; set; }
		public string Product { get; set; }
		public int Size { get; set; }
		public float Price { get; set; }
		public string ImagePath { get; set; }
		public bool IsDeleted { get; set; }
		

		public virtual Tukang Tukang { get; set; }
		public virtual Skills Skill { get; set; }
		public virtual IEnumerable<Transactions> Transactions { get; set; }
	}
}
