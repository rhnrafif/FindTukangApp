using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Applications.Services.PriceAppServices;
using FindTheBuilder.Applications.Services.PriceAppServices.DTO;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices.DTO;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices;
using FindTheBuilder.Applications.Services.TransactionDetailAppServices.DTO;
using FindTheBuilder.Databases.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace FindTheBuilder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerAppService _customerAppService;
		private readonly ITransactionAppService _transactionAppService;
		private readonly ITransactionDetailAppService _transactionDetailAppService;
		private readonly IPriceAppService _priceAppService;

		public CustomerController(ICustomerAppService customerAppService, 
			ITransactionAppService transactionAppService, 
			ITransactionDetailAppService transactionDetailAppService, 
			IPriceAppService priceAppService)
		{
			_customerAppService = customerAppService;
			_transactionAppService = transactionAppService;
			_transactionDetailAppService = transactionDetailAppService;
			_priceAppService = priceAppService;
		}

		// Customer
		[HttpPost("CreateCustomer")]
		//[Authorize(Roles = "Customer")]
		public IActionResult CreateCustomer([FromBody] CustomerDTO model)
		{
			try
			{
				if(model.Name != null)
				{
					_customerAppService.Create(model);
					return Requests.Response(this, new ApiStatus(200), null, "Success");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch(DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
			
		}
		
		[HttpPatch("UpdateCustomer")]
		//[Authorize(Roles = "Customer")]
		public IActionResult UpdateCustomer([FromBody] UpdateCustomerDTO model)
		{
			try
			{
				if (model.Name != null)
				{
					_customerAppService.Update(model);
					return Requests.Response(this, new ApiStatus(200), null, "Success");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}

		// Transaction
		[HttpPost("CreateTransaction")]
		//[Authorize(Roles = "Customer")]
		public IActionResult CreateTransaction([FromBody] TransactionDTO model)
		{
			try
			{
				if (model != null)
				{
					_transactionAppService.Create(model);
					return Requests.Response(this, new ApiStatus(200), null, "Success");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}

		[HttpPatch("UpdateTransaction")]
		//[Authorize(Roles = "Customer")]
		public IActionResult UpdateTransaction([FromBody] UpdateTransactionDTO model)
		{
			try
			{
				if (model != null)
				{
					_transactionAppService.Update(model);
					return Requests.Response(this, new ApiStatus(200), null, "Success");
				}
				return Requests.Response(this, new ApiStatus(400), null, "Error");
			}
			catch (DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}

		// Transaction Detail
		[HttpGet("GetAllTransaction")]
		//Authorize(Roles = "Customer")]
		public PagedResult<TransactionDetailDTO> GetAllTransaction([FromQuery] PageInfo pageInfo)
		{
			return _transactionDetailAppService.GetAllTransactions(pageInfo);
		}

		// Prices
		[HttpGet("GetAllPrice")]
		[AllowAnonymous]
		public PagedResult<PriceListDTO> GetAllPrice([FromQuery] PageInfo pageInfo)
		{
			return _priceAppService.GetPriceByProduct(pageInfo);
		}
	}
}
