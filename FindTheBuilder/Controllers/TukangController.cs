using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
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
	//[Authorize(Roles ="Tukang")]
	public class TukangController : ControllerBase
	{
		private readonly ITukangAppService _tukangAppService;
		private readonly IPriceAppService _priceAppService;

		public TukangController(ITukangAppService tukangAppService, 
			IPriceAppService priceAppService)
		{
			_tukangAppService = tukangAppService;
			_priceAppService = priceAppService;
		}

		// Tukang
		[HttpPost("CreateTukang")]
		[Authorize(Roles = "Tukang")]
		public IActionResult CreateTukang([FromBody] TukangDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = _tukangAppService.Create(model);
					if (res != null)
					{
						return Requests.Response(this, new ApiStatus(200), null, "Success");
					}
					return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}			
		}
		
		[HttpPatch("EditTukang")]
		[Authorize(Roles = "Tukang")]
		public IActionResult EditTukang([FromBody] UpdateTukangDTO model)		
		{
			try
			{
				if (model != null)
				{
					var res = _tukangAppService.Update(model);
					if (res != null)
					{
						return Requests.Response(this, new ApiStatus(200), null, "Success");
					}
					return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}

		// Prices
		[HttpPost("CreatePricing")]
		[Authorize(Roles = "Tukang")]
		public IActionResult CreatePricing([FromBody] PriceDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = _priceAppService.Create(model);
					if (res != null)
					{
						return Requests.Response(this, new ApiStatus(200), null, "Success");
					}
					return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}

		[HttpPatch("EditPricing")]
		[Authorize(Roles = "Tukang")]
		public IActionResult EditPricing([FromBody] UpdatePriceDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = _priceAppService.Update(model);
					if (res != null)
					{
						return Requests.Response(this, new ApiStatus(200), null, "Success");
					}
					return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}
		
		[HttpPatch("DeletePricing")]
		[Authorize(Roles = "Tukang")]
		public IActionResult DeletPricing(string product)
		{
			try
			{
				if (product != null)
				{
					var res = _priceAppService.Delete(product);
					if (res != null)
					{
						return Requests.Response(this, new ApiStatus(200), null, "Success");
					}
					return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}
	}
}
