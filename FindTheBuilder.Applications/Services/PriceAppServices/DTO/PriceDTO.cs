using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Services.PriceAppServices.DTO
{
	public class PriceDTO
	{
		public int TukangId { get; set; }
		public int SkillId { get; set; }
		public string Product { get; set; }
		public int Size { get; set; }
		public float Price { get; set; }
		public IFormFile Image { get;set; }
		public string? ImagePath { get; set; }
	}
}
