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
	//[Authorize(Roles ="Customer")]
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
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO model)
		{
			try
			{
				if(model.Name != "")
				{
					var isAdded = await _customerAppService.Create(model);
					if (isAdded)
					{
						return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), null, "Success")));
					}
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, "Error")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), null, "Error")));
			}
			catch(DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
			
		}
		
		[HttpPatch("UpdateCustomer")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO model)
		{
			try
			{
				if (model.Name != "")
				{
					var res = await _customerAppService.Update(model);
					if(res)
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

		// Transaction
		[HttpPost("CreateTransaction")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = await _transactionAppService.Create(model);
					if(res.Id != 0)
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

		[HttpPatch("UpdateTransaction")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionDTO model)
		{
			try
			{
				if (model != null)
				{
					var res = await _transactionAppService.Update(model);
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

		// Transaction Detail
		[HttpGet("GetAllTransactionDetails")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> GetAllTransaction([FromQuery] PageInfo pageInfo)
		{
			try
			{
				var data = await _transactionDetailAppService.GetAllTransactions(pageInfo);
				if(data.Data.Count() == 0)
				{
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "No Transaction")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), data, "Success")));
			}
			catch(DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}


		[HttpGet("GetActiveTransaction")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> GetActiveTransactionById(int transactionId)
		{
			try
			{
				var data = await _transactionAppService.GetTransActiveById(transactionId);
				if (data.Count() == 0)
				{
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "No Transaction")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), data, "Success")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}

		//TransactionDetail
		[HttpPost("create-detail-transaction")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> CreateTransDetail([FromBody] CreateTransactionDetailDTO model)
		{
			try
			{
				var data = await _transactionDetailAppService.CreateTransactionDetail(model);
				if (data.Id != 0)
				{
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), data, "Success")));
				}

				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(400), data, "Error")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}
		}

		// Prices
		[HttpGet("GetAllPrice")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllPrice([FromQuery] PageInfo pageInfo)
		{
			try
			{
				var data = await _priceAppService.GetAllPrice(pageInfo);
				if (data.Total == 0)
				{
					return await Task.Run(()=>(Requests.Response(this, new ApiStatus(404), null, "No Price List")));
				}
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(200), data, "Success")));
			}
			catch (DbException de)
			{
				return await Task.Run(()=>(Requests.Response(this, new ApiStatus(500), null, de.Message)));
			}			 
		}

		[HttpGet("DownloadProductImage")]
		[AllowAnonymous]
		public async Task<IActionResult> DownloadImage(int priceId)
		{
			try
			{
				var imageData = await _priceAppService.DownloadImage(priceId);
				if(imageData != null)
				{
					var bytes = await System.IO.File.ReadAllBytesAsync(imageData.ImagePath);
					return File(bytes, "image/jpeg", Path.GetFileName(imageData.ImagePath));
				}
				return await Task.Run(() => (Requests.Response(this, new ApiStatus(404), null, "No Product")));
			}
			catch(DbException de)
			{
				return await Task.Run(() => (Requests.Response(this, new ApiStatus(404), null, "Product image wasn't found on Server")));
			}
		}
	}
}
