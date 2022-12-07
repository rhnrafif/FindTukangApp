using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Applications.Services.ProductAppServices;
using FindTheBuilder.Applications.Services.ProductAppServices.DTO;
using FindTheBuilder.Applications.Services.SkillAppServices;
using FindTheBuilder.Applications.Services.SkillAppServices.DTO;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindTheBuilder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles ="Tukang")]
	public class TukangController : ControllerBase
	{
		private readonly ITukangAppService _tukangAppService;
		private readonly ISkillAppService _skillAppService;
		private readonly IProductAppService _productAppService;
		private readonly IPriceAppService _priceAppService;

		public TukangController(ITukangAppService tukangAppService, 
			ISkillAppService skillAppService, 
			IProductAppService productAppService, 
			IPriceAppService priceAppService)
		{
			_tukangAppService = tukangAppService;
			_skillAppService = skillAppService;
			_productAppService = productAppService;
			_priceAppService = priceAppService;
		}

		// Tukang

		[HttpPost("CreateTukang")]
		public Tukang CreateTukang([FromBody] TukangDTO model)		
		{			
			return _tukangAppService.Create(model);
		}
		
		[HttpPatch("EditTukang")]
		public Tukang EditTukang([FromBody] UpdateTukangDTO model)		
		{			
			return _tukangAppService.Update(model);
		}
		
		// Skill Tukang
		[HttpPost("CreateSkill")]
		public Skills CreateSkill([FromBody] SkillDTO model)		
		{			
			return _skillAppService.Create(model);
		}
		
		[HttpPatch("EditSkill")]
		public Skills EditSkill([FromBody] UpdateSkillDTO model)		
		{			
			return _skillAppService.Update(model);
		}
		
		[HttpDelete("DeleteSkill")]
		public Skills DeleteSkill(int id)		
		{			
			return _skillAppService.Delete(id);
		}
		
		// Product Tukang
		[HttpPost("CreateProduct")]
		public Products CreateProduct([FromBody] ProductDTO model)		
		{			
			return _productAppService.Create(model);
		}
		
		[HttpPatch("EditProduct")]
		public Products EditProduct([FromBody] UpdateProductDTO model)		
		{			
			return _productAppService.Update(model);
		}
		
		[HttpDelete("DeleteProduct")]
		public Products DeleteProduct(int id)		
		{			
			return _productAppService.Delete(id);
		}
		
		// Price Tukang
		[HttpPost("CreatePrice")]
		public Prices CreatePrice([FromBody] PriceDTO model)		
		{			
			return _priceAppService.Create(model);
		}
		
		[HttpPatch("EditPrice")]
		public Prices EditPrice([FromBody] UpdatePriceDTO model)		
		{			
			return _priceAppService.Update(model);
		}
		
		[HttpDelete("DeletePrice")]
		public Prices DeletePrice(int id)		
		{			
			return _priceAppService.Delete(id);
		}
	}
}
