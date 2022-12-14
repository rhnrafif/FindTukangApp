using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Applications.Services.SkillAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices;
using FindTheBuilder.Applications.Services.TukangAppServices.DTO;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace FindTheBuilder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TukangController : ControllerBase
	{
		private readonly ITukangAppService _tukangAppService;
		private readonly IPriceAppService _priceAppService;
		private readonly ISkillAppService _skillAppService;

		public TukangController(ITukangAppService tukangAppService, 
			IPriceAppService priceAppService, ISkillAppService skillAppService)
		{
			_tukangAppService = tukangAppService;
			_priceAppService = priceAppService;
			_skillAppService = skillAppService;
		}

		// Tukang
		[HttpPost("CreateTukang")]
		[Authorize(Roles = "Tukang")]
		public async Task<IActionResult> CreateTukang([FromBody] TukangDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = await _tukangAppService.Create(model);
					if (res != null)
					{
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), null, "Success")));
					}
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "Data Not Found")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Error")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}			
		}
		
		[HttpPatch("EditTukang")]
		[Authorize(Roles = "Tukang")]
		public async Task<IActionResult> EditTukang([FromBody] UpdateTukangDTO model)		
		{
			try
			{
				if (model != null)
				{
					var res = await _tukangAppService.Update(model);
					if (res.Id != 0)
					{
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), null, "Success")));
					}
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "Data Not Found")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Error")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}

		[HttpGet("skill-list")]
		//[Authorize(Roles = "Tukang")]
		public async Task<IActionResult> GetAllSkill()
		{
			try
			{
				var result = await _skillAppService.GetAllSkill();
				if(result.Count() != 0)
				{
					return await Task.Run(() => (Requests.Response(this, new ApiStatus(200), result, "Success")));
				}
				return await Task.Run(() => (Requests.Response(this, new ApiStatus(404), result, "Skill Not Found")));
			}
			catch (DbException de)
			{
				return await Task.Run(() => (Requests.Response(this, new ApiStatus(500), null, de.Message )));
			}
		}

		// Prices
		[HttpPost("CreatePricing")]
		//[Authorize(Roles = "Tukang")]
		[RequestSizeLimit(5 * 1024 * 1024)]
		public async Task<IActionResult> CreatePricing([FromForm] PriceDTO model)
		{
			try
			{
				if (model != null)
				{
					if (model.Image != null)
					{
						await _priceAppService.SaveImage(model);
					}
					var res = await _priceAppService.Create(model);
					if (res != null)
					{
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), null, "Success")));
					}
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "Data Not Found")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Error")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}

		[HttpPatch("EditPricing")]
		[Authorize(Roles = "Tukang")]
		public async Task<IActionResult> EditPricing([FromBody] UpdatePriceDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = await _priceAppService.Update(model);
					if (res.Id != 0)
					{
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), null, "Success")));
					}
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "Data Not Found")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Error")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}
		
		[HttpPatch("DeletePricing")]
		[Authorize(Roles = "Tukang")]
		public async Task<IActionResult> DeletPricing(string product)
		{
			try
			{
				if (product != null)
				{
					var res = await _priceAppService.Delete(product);
					if (res != null)
					{
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), null, "Success")));
					}
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "Data Not Found")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Error")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}
	}
}
