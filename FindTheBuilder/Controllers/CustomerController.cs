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
using Microsoft.AspNetCore.Mvc;

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
		public Customers CreateCustomer([FromBody] CustomerDTO model)
		{
			return _customerAppService.Create(model);
		}
		
		[HttpPatch("UpdateCustomer")]
		public Customers UpdateCustomer([FromBody] UpdateCustomerDTO model)
		{
			return _customerAppService.Update(model);
		}

		// Transaction
		[HttpPost("CreateTransaction")]
		public Transactions CreateTransaction([FromBody] TransactionDTO model)
		{
			return _transactionAppService.Create(model);
		}

		[HttpPatch("UpdateTransaction")]
		public Transactions UpdateTransaction([FromBody] UpdateTransactionDTO model)
		{
			return _transactionAppService.Update(model);
		}

		// Transaction Detail
		[HttpGet("GetAllTransaction")]
		public PagedResult<TransactionDetailDTO> GetAllTransaction([FromQuery] PageInfo pageInfo)
		{
			return _transactionDetailAppService.GetAllTransactions(pageInfo);
		}

		// Prices
		[HttpGet("GetAllPrice")]
		public PagedResult<PriceListDTO> GetAllPrice([FromQuery] PageInfo pageInfo)
		{
			return _priceAppService.GetPriceByProduct(pageInfo);
		}
	}
}
