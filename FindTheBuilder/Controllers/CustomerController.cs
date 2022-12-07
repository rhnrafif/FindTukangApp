using FindTheBuilder.Applications.Services.CustomerAppServices;
using FindTheBuilder.Applications.Services.CustomerAppServices.DTO;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindTheBuilder.Controllers
{
	[Route("api/customer")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerAppService _customerAppService;
		private readonly ITransactionAppService _transactionAppService;
		public CustomerController(ICustomerAppService customerAppService, ITransactionAppService transactionAppService)
		{
			_customerAppService = customerAppService;
			_transactionAppService = transactionAppService;
		}

		[HttpPost("create-customer")]
		public IActionResult CreateCustomer([FromBody] CustomerDTO model)
		{
			try
			{
				_customerAppService.Create(model);
				return Ok(new { Message = "Succes" });
			}
			catch
			{
				return Ok(new { Message = "Succes" });
			}
		}
	}
}
