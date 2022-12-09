using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.PriceAppServices.DTO
{
	public class PriceListDTO
	{		
		public string TukangName { get; set; }
		public string TukangSkill { get; set; }
		public string TukangProducts { get; set; }
		public int Size { get; set; }
		public float Price { get; set; }
		public bool IsDeleted { get; set; }
	}
}
