using FindTheBuilder.Applications.Helper;
using FindTheBuilder.Applications.Services.PaymentAppServices;
using FindTheBuilder.Applications.Services.TransactionAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace FindTheBuilder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentAppService _paymentAppService;
		private readonly ITransactionAppService _transactionAppService;
		public PaymentController(IPaymentAppService paymentAppService, ITransactionAppService transactionAppService)
		{
			_paymentAppService = paymentAppService;
			_transactionAppService = transactionAppService;
		}

		[HttpPost]
		public  async Task<IActionResult> Payment(int idTrans)
		{
			try
			{
				var res = await _paymentAppService.Post();
				if(res == null)
				{
					return Requests.Response(this, new ApiStatus(404), res, "Error");
				}
				await _transactionAppService.UpdatePayment(idTrans);
				return Requests.Response(this, new ApiStatus(200), res, "Success");
			}
			catch(DbException de)
			{
				return Requests.Response(this, new ApiStatus(500), null, de.Message);
			}
		}
	}
}
